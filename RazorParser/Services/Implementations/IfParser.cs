﻿using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace RazorParser
{
	public class IfParser : IIfParser
	{
		#region private fields
		IModelParser _modelParser;
		#endregion

		#region .ctors

		public IfParser(IModelParser modelParser){
			_modelParser = modelParser;		
		}
		#endregion

		#region public API
		public string ParseInline<T> (string html, T model)
		{
			var pattern = @"@\(\s*(?'condition'.+)\s*\?\s*(?'true'[^ ]+)\s*:\s*(?'false'[^ ]+)\s*\)";

			var a = Regex.Match(html, pattern);
			while(Regex.IsMatch(html,pattern)){
				var match = Regex.Match (html, pattern);
				string result = "";
				var condition = match.Groups ["condition"].Value;
				if (Parse (condition, model)) {					
					result = match.Groups ["true"].Value;
				} else {
					result = match.Groups ["false"].Value;
				}
				html = html.Replace (match.Value, result);

			}
			return html;
		}

		public string FindIfExpressions<T>(string html, T model){
			var pattern = @"@if\s*((?'a'\()[^{]*)((?'condition-a'\)))\s*((?'b'{)([^ ]|\s)*?)((?'ifpart-b'}))(\s*}\s*else\s*((?'c'{)([^ ]|\s)*)((?'else-c'})))*";

			var a = Regex.Match(html, pattern);
			while(Regex.IsMatch(html,pattern)){
				var match = Regex.Match (html, pattern);
				string result = "";
				var condition = match.Groups ["condition"].Value;
				if (Parse (condition, model)) {					
					result =  FindIfExpressions (match.Groups ["ifpart"].Value, model);
				} else {
					result = FindIfExpressions (match.Groups ["else"].Value, model);
				}
				html = html.Replace (match.Value, result);
			}
			return html;
		}
		#endregion

		#region utility methods
		bool Parse<T>(string condition, T model){
			var exp = FindParentheses (condition, model);
			var ifFunc = Expression.Lambda<Func<bool>> (exp).Compile ();
			return ifFunc ();
		}

		Expression FindParentheses<T> (string condition, T model)
		{
			var pattern =  @"((?'b'\()([^ ]|\s)+?)((?'content-b'\)))";
			var operands = new Dictionary<int, Expression> ();
			var operators = new Dictionary<int, Operator> ();


			foreach (var m in Regex.Matches (condition, pattern).Cast<Match> ()) {
				var value = m.Groups ["content"].Value;
				var exp = FindParentheses (value, model);
				operands.Add(m.Groups ["content"].Index, exp);
			}

			FillOperators (condition, operators);
			FillOperands (condition, operands, model);

			return BuildTree (operators, operands);
		}

		void FillOperators (string condition, Dictionary<int, Operator>  operators)
		{
			var pattern =  @"(\|+|==|!=|>|<|=>|=<|&+)+";
			foreach (var m in Regex.Matches (condition, pattern).Cast<Match> ()) {
				operators.Add (m.Index, ExpressionHelpers.ToOperator (m.Value));
			}
		}

		void FillOperands<T> (string condition, Dictionary<int, Expression>  operands, T model)
		{
			//should skip parentheses 
			var pattern =  @"\(.*?\)|([^ !<|&>=]+)";
			foreach (var m in Regex.Matches (condition, pattern).Cast<Match> ()) {
				// 0 group will be null if there is a parenthesises
				if (String.IsNullOrEmpty (m.Groups [0].Value))
					continue;
				if (m.Groups [0].Value.Equals ("null")) {
					operands.Add (m.Index, Expression.Constant (null));
				} else {
					operands.Add (m.Index, Expression.Constant (_modelParser.InjectSingleField(m.Groups [0].Value, model)));
				}
			}
		}

		Expression BuildTree (Dictionary<int, Operator> operators, Dictionary<int, Expression> operands)
		{
			try{
			foreach (var op in operators.OrderByDescending(s=>s.Value.Priority)) {
				Expression leftOperand = null;
				Expression rightOperand = null;
				BinaryExpression exp = null;
				FindNeighbours (op.Key, operands, out leftOperand, out rightOperand);

				var right = rightOperand as ConstantExpression;
				var left = leftOperand as ConstantExpression;

					if (right!=null && left!=null && ((right.Value == null && left.Type.IsValueType) || (left.Value == null && right.Type.IsValueType))) {
					//should cast value type to nullable
						if(left.Type.IsValueType){
							exp = Expression.MakeBinary (op.Value.Type, Expression.Constant(left.Value, typeof(Nullable<>).MakeGenericType(left.Type)), right);
						} else{
							exp = Expression.MakeBinary (op.Value.Type, Expression.Constant(right.Value, typeof(Nullable<>).MakeGenericType(right.Type)), left);
						}
				} else {
					exp = Expression.MakeBinary (op.Value.Type, leftOperand, rightOperand);
				}
				operands.Add (op.Key, exp);
			}
			if(operands.Count!=1)
				throw new Exception("Operands more than 1");

			return operands.First ().Value;
			} catch(Exception e){
			}
			return null;
		}

		void FindNeighbours (int key, Dictionary<int, Expression> operands, out  Expression leftOperand, out  Expression rightOperand)
		{
			var leftNullable = operands
				.Where (item => item.Key < key)
				.Select (e => (KeyValuePair<int, Expression>?)e)
				.Select (n => new { n, distance = key - n.Value.Key})
				.OrderBy (p => p.distance)
				.First().n;
			
			if (leftNullable == null) {
				throw new ArgumentException ("Left operand not found");
			}
			var left = leftNullable.Value;

			operands.Remove (left.Key);
			leftOperand = left.Value;

			var rightNullable = operands
				.Where(item=>item.Key>key)
				.Select(e => (KeyValuePair<int, Expression>?) e)
				.Select (n => new { n, distance = n.Value.Key - key })
				.OrderBy (p => p.distance)
				.First().n;
			if (rightNullable == null) {
				throw new ArgumentException ("Right operand not found");
			}
			var right = rightNullable.Value;
			operands.Remove (right.Key);
			rightOperand = right.Value;
		}
		#endregion
	}
}

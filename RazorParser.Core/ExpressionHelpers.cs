using System;
using System.Linq.Expressions;

namespace RazorParser
{
//	<  >  <=  >=  	6	
//
//	==  !=			5	
//
//	&				4	
//
//	^				3	
//
//	|				2	
//
//	&&				1	
//
//	||				0	

	public static class ExpressionHelpers
	{
		public static Operator ToOperator( string str){
			switch(str){
				case "<":
					return new Operator (ExpressionType.LessThan, 6);
				case "<=":
					return new Operator (ExpressionType.LessThanOrEqual, 6);
				case ">":
					return new Operator (ExpressionType.GreaterThan, 6);
				case ">=":
					return new Operator (ExpressionType.GreaterThanOrEqual, 6);
				case "==":
					return new Operator (ExpressionType.Equal, 5);
				case "!=":
					return new Operator (ExpressionType.NotEqual, 5);
				case "||":
					return new Operator (ExpressionType.OrElse, 0);
				case "|":
					return new Operator (ExpressionType.Or, 2);
				case "&&":
					return new Operator (ExpressionType.AndAlso, 1);
				case "&":
					return new Operator (ExpressionType.And, 4);
			}
			return null;
		}
	}
}


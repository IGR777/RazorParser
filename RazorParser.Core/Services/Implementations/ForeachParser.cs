using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace RazorParser
{
	public class ForeachParser : IForeachParser
	{
		IModelParser _modelParser;

		public ForeachParser (IModelParser modelParser)
		{
			_modelParser = modelParser;
		}

		public string Parse<T> (string expression, T model)
		{
			return FindForeachExpressions (expression, model);
		}

		string FindForeachExpressions<T> (string html, T model)
		{
			try {
				var pattern = @"@foreach\s*((?'a'\()[^{]*)((?'condition-a'\)))\s*((?'b'{)([^ ]|\s)*?)((?'content-b'}))";

				IModelLocator modelLocator = ServiceLocator.Resolve<IModelLocator> ();
				var result = Regex.Replace (html, pattern, match => {
					var condition = match.Groups ["condition"].Value;
					var content = match.Groups ["content"].Value;
					Debug.WriteLine ("FindForeachExpressions expression-{0}\ncontent-{1}", condition, content);
					return ProceedForeachExpression (model, condition, content, modelLocator);
				});
				return result;
			} catch (Exception e) {
				throw;
			}
		}

		string ProceedForeachExpression<T> (T model, string condition, string content, IModelLocator modelLocator)
		{
			string result = "";
			IEnumerable<object> enumerable;
			string stringEnumerator;
			FindForeachComponents (condition, model, out stringEnumerator, out enumerable);
			var numerator = enumerable.GetEnumerator ();
			numerator.Reset ();
			var ind = 0;
			while (numerator.MoveNext ()) {
				var current = numerator.Current;
				var newKey = modelLocator.AddUntilAdded (stringEnumerator, current);
				var subResult = ChangeModelNames (newKey, stringEnumerator, content);
				result += subResult + Environment.NewLine;
			}
			return result;
		}

		string ChangeModelNames (string newKey, string oldKey, string content)
		{
			var pattern = @"@" + oldKey;
			return Regex.Replace (content, pattern, "@" + newKey);
		}

		void FindForeachComponents<T> (string condition, T model, out string enumerator, out IEnumerable<object> enumerable)
		{			
			var pattern = @".+\s+(?'tor'[^ ]+)\s*in\s+(?'rable'@Model..+)";
			var match = Regex.Match (condition, pattern);
			enumerator = match.Groups ["tor"].Value;
			var strEnumerable = match.Groups ["rable"].Value;
			enumerable = _modelParser.InjectSingleField (strEnumerable, model) as IEnumerable<object>; 
		}
	}
}


using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace RazorParser
{
	public static class ForeachParser
	{
		public static string Parse<T> (string expression, T model)
		{
			return null;
		}

		public static string FindForeachExpressions<T> (string html, T model)
		{
			try {
				var pattern = @"@foreach\s*((?'a'\()[^{]*)((?'condition-a'\)))\s*((?'b'{)([^ ]|\s)*?)((?'content-b'}))";

				while (Regex.IsMatch (html, pattern)) {
					var match = Regex.Match (html, pattern);
					var condition = match.Groups ["condition"].Value;
					var content = match.Groups ["content"].Value;
					IEnumerable enumerable;
					string enumerator;
					FindForeachComponents (condition, model, out enumerator, out enumerable);
//					html = html.Replace (match.Value, result);

				}
			} catch (Exception e) {
			}
			return html;
		}

		static void FindForeachComponents<T> (string condition, T model, out string enumerator, out IEnumerable enumerable)
		{
			var pattern = @".+\s+(?'tor'[^ ]+)\s*in\s+@Model.(?'rable'.+)";
		}
	}
}


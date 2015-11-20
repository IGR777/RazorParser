using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RazorParser
{
	public static class RazorParser
	{
		public static string Parse<T>(string html, T model){
			var result = FindIfExpressions (html, model);
			//			var result = InjectModelFields (html, model);

			return result;
		}

		static string FindIfExpressions<T>(string html, T model){
			try{
				var pattern = @"@if\s*((?'a'\()[^{]*)((?'condition-a'\)))\s*((?'b'{)([^ ]|\s)*?)((?'ifpart-b'}))(\s*}\s*else\s*((?'c'{)([^ ]|\s)*)((?'else-c'})))*";
//				var a = Regex.IsMatch (html, pattern);


				var a = Regex.Match(html, pattern);
				while(Regex.IsMatch(html,pattern)){
					var match = Regex.Match (html, pattern);
					string result = "";
//					var condition = InjectModelFields(match.Groups ["condition"].Value, model);
					var condition = match.Groups ["condition"].Value;
					if (ExecuteExpression (condition, model)) {
						result =  FindIfExpressions (match.Groups ["ifpart"].Value, model);
					} else {
						result = FindIfExpressions (match.Groups ["else"].Value, model);
					}
					html = html.Replace (match.Value, result);

				}
			}catch(Exception e){
			}
			return html;

		}

		static bool ExecuteExpression<T> (string value, T model)
		{
			return IfParser.Parse (value, model);
		}
	}

}


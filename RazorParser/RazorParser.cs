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
			using (var modelLocator = ServiceLocator.Resolve<IModelLocator> ()) {
				var ifParser = ServiceLocator.Resolve<IIfParser> ();
				var result = ifParser.FindIfExpressions (html, model);

				return result;
			}
		}
	}

}


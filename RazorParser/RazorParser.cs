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
		public static string Parse<T> (string html, T model)
		{
			using (var modelLocator = ServiceLocator.Resolve<IModelLocator> ()) {
				//remove razor defines
				html = html.Remove (0, html.IndexOf ("<html>"));

				modelLocator.AddModel ("Model", model);

				//step 1: foreach
				var foreachResult = ServiceLocator.Resolve<IForeachParser> ().Parse (html, model);

				//step 2: if
				var ifParser = ServiceLocator.Resolve<IIfParser> ();
				var ifResult = ifParser.Parse (foreachResult, model);

				//step 3: inline if
				var inlineIfResult = ifParser.ParseInline (ifResult, model);

				//step 4: model
				var modelResult = ServiceLocator.Resolve<IModelParser> ().InjectModelFieldsInHtml (inlineIfResult);
				return modelResult;
			}
		}
	}

}


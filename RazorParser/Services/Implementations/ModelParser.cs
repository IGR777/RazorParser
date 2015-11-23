using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace RazorParser
{
	public class ModelParser : IModelParser
	{
		public string InjectModelFieldsInHtml<T> (string html, T model)
		{
			var pattern = @"@*Model.(?'PropertyName'[A-Za-z0-9]+)\.*(?'MethodName'[A-Za-z0-9]*)";
			var props = model.GetType ().GetTypeInfo ().DeclaredProperties;
			var result = Regex.Replace (html, pattern, ((Match match) =>  {
				var name = match.Groups["PropertyName"].Value;
				var method = match.Groups["MethodName"].Value;
				var value = props.FirstOrDefault (item => item.Name.Equals (name)).GetValue (model);
				if(value!=null && !String.IsNullOrEmpty(method)){
					if(value is IEnumerable){
						var exMethod = GetLinqExtensions(method);
						if(exMethod!=null){
							return exMethod.Invoke(value, new object[]{value}).ToString();
						}
					} else{
						var valueProp = value.GetType().GetRuntimeProperty(method);
						if(valueProp!=null)
							return valueProp.GetValue(value).ToString();
					}
				}
				return value != null ? value.ToString () : "null";
			}));
			return result;
		}

		object GetStaticClass (string field)
		{
			var pattern = @"@*(?'ClassName'[A-Za-z0-9]+)\.*(?'PropertyName'[A-Za-z0-9]+)*\.*(?'MethodName'[A-Za-z0-9]+)*";
			var match = Regex.Match (field, pattern);
			if(match==null)
				throw new ArgumentException (String.Format ("ModelParser GetStaticClass - field {0} isn't matched", field));

			var className = match.Groups ["ClassName"].Value;
			var firstMemberName = match.Groups ["PropertyName"].Value;
			var secondMemberName = match.Groups ["MethodName"].Value;
			//some static value, no type needed
			if (String.IsNullOrEmpty (firstMemberName) && String.IsNullOrEmpty (secondMemberName)) {
				int intValue;
				return Int32.TryParse (className, out intValue) ? (object)intValue : className;
			}
			var type = Assembly.GetAssembly (field.GetType ()).GetTypes ().FirstOrDefault (t => t.IsSealed && t.IsPublic && t.Name.Equals(className));
			if(type==null)
				throw new ArgumentException (String.Format ("ModelParser GetStaticClass - class not found", className));
			var member = type.GetRuntimeField(firstMemberName);
			var value = member.GetValue (className);
			if (!String.IsNullOrEmpty (secondMemberName)) {
				var member2 = value.GetType().GetRuntimeProperty (secondMemberName);
				return member2.GetValue (value);
			} 
			return value;

		}

		object GetModelValue<T> (string field, string pattern, T model)
		{

			var props = model.GetType ().GetTypeInfo ().DeclaredProperties;
			var match = Regex.Match (field, pattern);
			var name = match.Groups ["PropertyName"].Value;
			var method = match.Groups ["MethodName"].Value;
			var value = props.FirstOrDefault (item => item.Name.Equals (name)).GetValue (model);
			if (value == null)
				return value;
			if (!String.IsNullOrEmpty (method)) {
				if (value is IEnumerable) {
					var exMethod = GetLinqExtensions (method);
					if (exMethod != null) {
						return exMethod.Invoke (value, new object[] {
							value
						});
					}
				}
				else {
					var valueProp = value.GetType ().GetRuntimeProperty (method);
					if (valueProp != null)
						return valueProp.GetValue (value);
				}
			}
			return value;
		}

		public object InjectSingleField<TModel> (string field, TModel model)
		{
			var pattern = @"@*Model.(?'PropertyName'[A-Za-z0-9]+)\.*(?'MethodName'[A-Za-z0-9]*)";
			if (!Regex.IsMatch (field, pattern)) {
				return GetStaticClass (field);			
			} else {
				return GetModelValue (field, pattern, model);
			}
		}


		MethodInfo GetLinqExtensions(string methodName){
			var method =  typeof(System.Linq.Enumerable)
				.GetTypeInfo ()
				.DeclaredMethods
				.FirstOrDefault (item => (item.IsStatic | item.IsPublic) && item.Name.Equals (methodName));

			if(method!=null && method.IsGenericMethod){
				method = method.MakeGenericMethod(typeof(Object));
			} 
			return method;
		}
	}
}


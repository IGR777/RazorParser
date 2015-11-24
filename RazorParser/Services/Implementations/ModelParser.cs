using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace RazorParser
{
	public class ModelParser : IModelParser
	{
		#region private fields

		IModelLocator _modelLocator;

		#endregion

		#region .ctors

		public ModelParser (IModelLocator modelLocator)
		{
			_modelLocator = modelLocator;
		}

		#endregion

		#region public API

		public string InjectModelFieldsInHtml (string html)
		{
//			try {
//			var pattern = @"@\(*(?'ModelName'[A-Za-z0-9]+).(?'PropertyName'[A-Za-z0-9]+)\.*(?'MethodName'[A-Za-z0-9]*)\)*";
			var pattern = @"@\(*(?'ModelName'[A-Za-z0-9]+).(?'PropertyName'[A-Za-z0-9]+)\.*(?'MethodName'[A-Za-z0-9]*)(?'other'\.[^ <>]+)*\)*";
//			var props = model.GetType ().GetTypeInfo ().DeclaredProperties;
			var _propCache = new Dictionary<Type,IEnumerable<PropertyInfo>> ();
			var result = Regex.Replace (html, pattern, ((Match match) => {
				var modelName = match.Groups ["ModelName"].Value;
				var name = match.Groups ["PropertyName"].Value;
				var method = match.Groups ["MethodName"].Value;

				var other = match.Groups ["other"].Value;
				var model = _modelLocator.GetModel (modelName);
				var props = GetModelProperties (model.GetType (), _propCache);
				var pinfo = props.FirstOrDefault (item => item.Name.Equals (name));
				var value = pinfo.GetValue (model);
				object res = null;
				if (value != null && !String.IsNullOrEmpty (method)) {
					if (method.Equals ("Value") && Nullable.GetUnderlyingType (pinfo.PropertyType) != null) {
						res = value;
					} else {
						var valueProp = value.GetType ().GetRuntimeProperty (method);
						if (valueProp != null)
							res = valueProp.GetValue (value);
					}
				}
				if (res == null)
					res = value != null ? value.ToString () : "null";
				if (!String.IsNullOrEmpty (other))
					//field have > 3 Properties/methods calls
					return ProceedBigData (res, other);
				else
					return res.ToString ();
				//				
			}));

			_propCache.Clear ();

			return result;
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

		#endregion

		#region utility fields

		public static T ChangeType<T> (object value)
		{
			var t = typeof(T);

			if (t.IsGenericType && t.GetGenericTypeDefinition ().Equals (typeof(Nullable<>))) {
				if (value == null) { 
					return default(T); 
				}

				t = Nullable.GetUnderlyingType (t);
			}

			return (T)Convert.ChangeType (value, t);
		}


		string ProceedBigData (object res, string other)
		{
			var acts = other.Split (new[]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var act in acts) {
				if (act.Contains ('(')) {
					res = CallMethod (res, act);
				} else {
					res = CallProperty (res, act);
				}
			}
			return res.ToString ();//throw new NotImplementedException ();
		}

		object CallProperty (object res, string act)
		{
			var prop = res.GetType ().GetTypeInfo ().DeclaredProperties.FirstOrDefault ((item) => item.Name.Equals (act));
			return prop.GetValue (res);
		}

		object CallMethod (object res, string act)
		{
			var startInd = act.IndexOf ('(') + 1;
			var paramsSubs = act.Substring (startInd, act.IndexOf (')') - startInd);
			var strParams = paramsSubs.Split (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			var prms = new List<object> ();
			foreach (var p in strParams) {
				if (p.StartsWith ("\"")) {
					prms.Add (p.Trim (new[]{ '\"' }));
				}
			}

			var methodName = act.Substring (0, startInd - 1);
			var mths = res.GetType ().GetTypeInfo ().DeclaredMethods;
			//find all methods
			var methods = res.GetType ().GetTypeInfo ().DeclaredMethods.Where ((item) => item.Name.Equals (methodName));
			//find method with the same number of parameters
			var method = methods.FirstOrDefault (m => m.GetParameters ().Count () == prms.Count);
			return method.Invoke (res, prms.ToArray ());
		}

		IEnumerable<PropertyInfo> GetModelProperties (Type type, Dictionary<Type, IEnumerable<PropertyInfo>> _cache)
		{
			if (_cache.ContainsKey (type))
				return _cache [type];
			
			var props = type.GetTypeInfo ().DeclaredProperties;
			_cache.Add (type, props);
			return props;
		}

		object GetStaticClass (string field)
		{
			var pattern = @"@*(?'ClassName'[A-Za-z0-9]+)\.*(?'PropertyName'[A-Za-z0-9]+)*\.*(?'MethodName'[A-Za-z0-9]+)*";
			var match = Regex.Match (field, pattern);
			if (match == null)
				throw new ArgumentException (String.Format ("ModelParser GetStaticClass - field {0} isn't matched", field));

			var className = match.Groups ["ClassName"].Value;
			var firstMemberName = match.Groups ["PropertyName"].Value;
			var secondMemberName = match.Groups ["MethodName"].Value;
			//some static value, no type needed
			if (String.IsNullOrEmpty (firstMemberName) && String.IsNullOrEmpty (secondMemberName)) {
				int intValue;
				return Int32.TryParse (className, out intValue) ? (object)intValue : className;
			}
			var type = Assembly.GetAssembly (field.GetType ()).GetTypes ().FirstOrDefault (t => t.IsSealed && t.IsPublic && t.Name.Equals (className));
			if (type == null)
				throw new ArgumentException (String.Format ("ModelParser GetStaticClass - class not found", className));
			var member = type.GetRuntimeField (firstMemberName);
			var value = member.GetValue (className);
			if (!String.IsNullOrEmpty (secondMemberName)) {
				var member2 = value.GetType ().GetRuntimeProperty (secondMemberName);
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
				} else {
					var valueProp = value.GetType ().GetRuntimeProperty (method);
					if (valueProp != null)
						return valueProp.GetValue (value);
				}
			}
			return value;
		}

		MethodInfo GetLinqExtensions (string methodName)
		{
			var method = typeof(System.Linq.Enumerable)
				.GetTypeInfo ()
				.DeclaredMethods
				.FirstOrDefault (item => (item.IsStatic | item.IsPublic) && item.Name.Equals (methodName));

			if (method != null && method.IsGenericMethod) {
				method = method.MakeGenericMethod (typeof(Object));
			} 
			return method;
		}

		#endregion
	}
}


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace RazorParser
{
	public static class ServiceLocator{
		public static void Add<TContract, TService>() where TService : new()
		{
			Locator.Instance.Add<TContract, TService> ();
		}

		public static T Resolve<T>()
		{
			return Locator.Instance.Resolve<T> ();
		}

		class Locator
		{
			static readonly Lazy<Locator> instance = 
				new Lazy<Locator>(() => {
					var loc =  new Locator();
					loc.Add<IModelLocator, ModelLocator>();
					loc.Add<IIfParser, IfParser>();
					loc.Add<IModelParser, ModelParser>();
					loc.Add<IForeachParser, ForeachParser>();
					return loc;
				});
			
			readonly Dictionary<Type, Lazy<object>> registeredServices = 
				new Dictionary<Type, Lazy<object>>();

			public static Locator Instance
			{
				get { return instance.Value; }
			}

			public void Add<TContract, TService>()
			{
				this.registeredServices[typeof(TContract)] = 
					new Lazy<object>(() => Create(typeof(TService)));
			}

			public T Resolve<T>()
			{
				Lazy<object> service;
				if (registeredServices.TryGetValue(typeof(T), out service)) {
					return (T)service.Value;
				}

				throw new Exception("No service found for " + typeof(T).Name);
			}

			object Create(Type type)
			{
				TypeInfo typeInfo = type.GetTypeInfo();

				Lazy<object> creator;
				if (registeredServices.TryGetValue(type, out creator))
					return registeredServices[type].Value;

				var ctors = typeInfo.DeclaredConstructors.Where(c => c.IsPublic).ToArray();
				var ctor = ctors.FirstOrDefault(c => c.GetParameters().Length == 0);
				if (ctor != null)
					return Activator.CreateInstance(type);

				// Pick the first constructor found and create any parameters.
				ctor = ctors[0];
				List<object> parameters = new List<object>();
				foreach (var p in ctor.GetParameters())
					parameters.Add(Create(p.ParameterType));

				return Activator.CreateInstance(type, parameters.ToArray());
			}
		}
	}
}


using System;
using System.Collections.Generic;

namespace RazorParser
{
	public class ModelLocator : IModelLocator
	{
		Dictionary<string, object> _modelCache = new Dictionary<string, object> ();

		public void AddModel (string key, object model)
		{
			_modelCache.Add (key, model);
		}

		public string AddUntilAdded (string key, object model)
		{
			var newKey = key;
			for (int i = 0; i < _modelCache.Count; i++) {				
				if (_modelCache.ContainsKey (newKey)) {
					newKey = key + i;
				} else {
					break;
				}
			}
			_modelCache.Add (newKey, model);
			return newKey;
		}

		public object GetModel (string key)
		{

			return _modelCache [key];
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			Dispose (true);
		}

		void Dispose (bool disposing)
		{
			if (disposing) {
				_modelCache.Clear ();
			}
		}

		#endregion
	}
}


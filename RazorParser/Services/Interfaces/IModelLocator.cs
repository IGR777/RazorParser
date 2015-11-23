using System;

namespace RazorParser
{
	public interface IModelLocator : IDisposable
	{
		void AddModel(string key, object model);

		string AddUntilAdded(string key, object model);

		object GetModel(string key);
	}
}


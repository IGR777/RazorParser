using System;

namespace RazorParser
{
	public interface IModelParser
	{
		string InjectModelFieldsInHtml<T> (string html, T model);

		object InjectSingleField<TModel> (string field, TModel model);
	}
}


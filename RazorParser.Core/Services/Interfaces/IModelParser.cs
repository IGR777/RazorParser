using System;

namespace RazorParser
{
	public interface IModelParser
	{
		string InjectModelFieldsInHtml (string html);

		object InjectSingleField<TModel> (string field, TModel model);
	}
}


using System;

namespace RazorParser
{
	public interface IIfParser
	{
		string Parse<T> (string expression, T model);

		string ParseInline<T> (string expression, T model);
	}
}


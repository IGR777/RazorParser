using System;

namespace RazorParser
{
	public interface IForeachParser
	{
		string Parse<T> (string expression, T model);
	}
}


using System;
using System.Linq.Expressions;

namespace RazorParser
{
	public class Operator
	{
		public Operator(ExpressionType type, int priority){
			Type = type;
			Priority = priority;
		}

		public ExpressionType Type{ get; set; }
		public int Priority{ get; set; }
	}
}


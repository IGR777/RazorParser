using NUnit.Framework;
using System;
using System.Collections;

namespace RazorParser.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test,TestCaseSource(typeof(TestCasesClass),"TestCases")]
		public bool TestIfParser (string condition, object model)
		{
			var result = IfParser.Parse (condition, model);
			return result;
		}
	}

	public class TestCasesClass
	{
		public static IEnumerable TestCases
		{
			get
			{
				yield return new TestCaseData( @"@Model.Gears != 0", new{Gears = 0}).Returns( false );
				yield return new TestCaseData( @"@Model.Seller != null", new {Seller ="123"} ).Returns( true );
				yield return new TestCaseData( @"Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate =DateTime.Now} ).Returns( true );
				yield return new TestCaseData( @"Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate =DateTime.MinValue} ).Returns( false );
				yield return new TestCaseData( @"Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = (DateTime?)null} ).Returns( false );
				yield return new TestCaseData( @"@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate =DateTime.Now} ).Returns( true );
				yield return new TestCaseData( @"@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate =DateTime.MinValue} ).Returns( false );
				yield return new TestCaseData( @"@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = (DateTime?)null} ).Returns( false );
				yield return new TestCaseData( @"(@Model.MOTExpiryDate != null && Model.IsTrue == true) || DateTime.MinValue !=DateTime.MaxValue", 
					new {MOTExpiryDate = DateTime.Now, IsTrue = true} ).Returns( true );

			}
		}  
	}
}


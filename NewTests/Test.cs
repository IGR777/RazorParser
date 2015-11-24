using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using NewTests;

namespace RazorParser.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test,TestCaseSource (typeof(TestCasesClass), "TestCases")]
		public string TestIfParser (string condition, object model)
		{
			var result = ServiceLocator.Resolve<IIfParser> ().FindIfExpressions (condition, model);
			return result;
		}

		[Test,TestCaseSource (typeof(ModelTestCasesClass), "TestCases")]
		public void TestModelParser (string condition, object model, int count)
		{
			var en = ServiceLocator.Resolve<IModelParser> ().InjectSingleField (condition, model) as IEnumerable<object>;
			Assert.AreEqual (en.Count (), count);
		}

		[Test,TestCaseSource (typeof(ForeachTestCasesClass), "TestCases")]
		public string TestForeachParser (string condition, object model)
		{
			var result = ServiceLocator.Resolve<IForeachParser> ().Parse (condition, model);
			return result.Trim ();
		}

		[Test,TestCaseSource (typeof(IfInlineTestCasesClass), "TestCases")]
		public string TestIfParserInline (string condition, object model)
		{
			var result = ServiceLocator.Resolve<IIfParser> ().ParseInline (condition, model);
			return result;
		}
	}

	public class TestCasesClass
	{
		public static IEnumerable TestCases {
			get {
				yield return new TestCaseData (@"@if (@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue){Yes}"
					, new { MOTExpiryDate = DateTime.Now}).Returns ("Yes");
				yield return new TestCaseData (@"@if (@Model.MOTExpiryDate != null && (Model.MOTExpiryDate != DateTime.MinValue || Model.MOTExpiryDate != DateTime.MaxValue)){Yes}"
					, new { MOTExpiryDate = DateTime.Now}).Returns ("Yes");
//				yield return new TestCaseData (@"@Model.Seller != null", new {Seller = "123"}).Returns (true);
//				yield return new TestCaseData (@"Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = DateTime.Now}).Returns (true);
//				yield return new TestCaseData (@"Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = DateTime.MinValue}).Returns (false);
//				yield return new TestCaseData (@"Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = (DateTime?)null}).Returns (false);
//				yield return new TestCaseData (@"@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = DateTime.Now}).Returns (true);
//				yield return new TestCaseData (@"@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = DateTime.MinValue}).Returns (false);
//				yield return new TestCaseData (@"@Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue", new {MOTExpiryDate = (DateTime?)null}).Returns (false);
//				yield return new TestCaseData (@"(@Model.MOTExpiryDate != null && Model.IsTrue == true) || DateTime.MinValue !=DateTime.MaxValue", 
//					new {MOTExpiryDate = DateTime.Now, IsTrue = true}).Returns (true);

			}
		}
	}

	public class IfInlineTestCasesClass
	{
		public static IEnumerable TestCases {
			get {
				yield return new TestCaseData (@"@(Model.Gears != 0 ? Yes : No)", new{Gears = 0}).Returns ("No");
				yield return new TestCaseData (@"@((Model.MOTExpiryDate != null && Model.MOTExpiryDate != DateTime.MinValue) ? Yes : No)"
					, new { MOTExpiryDate = DateTime.Now}).Returns ("Yes");
			}
		}
	}

	public class ForeachTestCasesClass
	{
		public static IEnumerable TestCases {
			get {
				yield return new TestCaseData (@"@foreach (var tyr in @Model.Tyres)
{
                    <tr>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr.Location</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr.TyreWheelType</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr.Depth</p></td>
                        <td class=""tr1 td7""><p class=""p0 ft3"">@tyr.TyreCondition</p></td>
                        <td class=""tr1 td8""><p class=""p0 ft3"">@tyr.TyreSize @tyr.TyreRim @tyr.TyreSpeed</p></td>
                        <td class=""tr1 td9""><p class=""p0 ft3"">@tyr.Brand</p></td>
                    </tr> 
}"
					, new{
						Tyres = new[] {
					new{
								Location = "Location1",
								TyreWheelType = "TyreWheelType1",
								Depth = "Depth1",
								TyreCondition = "TyreCondition1",
								TyreSize = "TyreSize1",
								TyreRim = "TyreRim1",
								TyreSpeed = "TyreSpeed1",
								Brand = "Brand1",
							},
					new{
								Location = "Location2",
								TyreWheelType = "TyreWheelType2",
								Depth = "Depth2",
								TyreCondition = "TyreCondition2",
								TyreSize = "TyreSize2",
								TyreRim = "TyreRim2",
								TyreSpeed = "TyreSpeed2",
								Brand = "Brand2",
							},
					new{
								Location = "Location3",
								TyreWheelType = "TyreWheelType3",
								Depth = "Depth3",
								TyreCondition = "TyreCondition3",
								TyreSize = "TyreSize3",
								TyreRim = "TyreRim3",
								TyreSpeed = "TyreSpeed3",
								Brand = "Brand3",
							}
				}
					}).Returns (BigTestTextFile.GetText ());
			}
		}
	}

	public class ModelTestCasesClass
	{
		public static IEnumerable TestCases {
			get {
				yield return new TestCaseData (@"@Model.Tyres", new {
					Tyres = new[] { 
					new EnumerableItem{ Id = 0, Description = "0" },
					new EnumerableItem{ Id = 1, Description = "1" },
					new EnumerableItem{ Id = 2, Description = "2" } 
				}
				}, 3);
			}
		}
	}

	public class EnumerableItem
	{
		public int Id { get; set; }

		public string Description { get; set; }
	}
}


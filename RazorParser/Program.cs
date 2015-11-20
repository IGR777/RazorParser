using System;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;

namespace RazorParser
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var str = RazorParser.Parse (RazorText.Value, new  {
				Make = "MyMake"
				, VIN = "23423423423"
				, Seller = "234"
				,  MOTExpiryDate = (DateTime?)DateTime.Now
				, MaxOccupancy =0
				, Gears =123
				, PaintType = (Type)null
				, TrimColour = (Type)null
				, InteriorTrim = (Type)null
					, Mileage =0
					, OdometrUnit = (Type)null
					, SpeedoChangeReason = (Type)null
					, Fuel = (Type)null
					, FuelLevel = (Type)null
					, OilLevel = (Type)null
					, OilLevelAction = (Type)null
					, WaterLevel = (Type)null
					, WaterLevelAction = (Type)null
					, EngineCondition = (Type)null
					, Tyres = new List<object>{1,2,3}
					, DamageWaiver = (Type)null
			});
		
		}

	}
}

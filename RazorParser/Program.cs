using System;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

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
				, MaxOccupancy = 0
				, Gears = 123
					, Mileage = 0
					, Fuel = (Type)null
					, FuelLevel = (Type)null
					, OilLevel = (Type)null
					, OilLevelAction = (Type)null
					, WaterLevel = (Type)null
					, WaterLevelAction = (Type)null
					, EngineCondition = (Type)null
					, Tyres = new List<object> {
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
					, DamageWaiver = (Type)null
					, Damage = new List<object> { 
				new{
							Component = "Component1",
							Fault = "Fault1"
						},
				new{
							Component = "Component2",
							Fault = "Fault2"
						},
				new{
							Component = "Component3",
							Fault = "Fault3"
						}
			}
					, HandBook = true
					, ServBook = true
					, V5 = false
					,LockingWheelNut = true
					, ServiceHistory = "History"
					, Registration = "Registration"
					, Model = "Model"
					, Variant = "Variant"
					, BodyPlan = "BodyPlan"
					, GearBox = "GearBox"
					, EngineSize = "EngineSize"
					, EngineSizeUnit = "EngineSizeUnit"
					, Colour = "Colour"
					, PaintType = "PaintType"
					,TrimColour = "TrimColour"
					, InteriorTrim = "InteriorTrim"
					, OdometrUnit = "OdometrUnit"
					, SpeedoChangeReason = "SpeedoChangeReason"
					, SpareKeys = "SpareKeys"
					, InspectionCondition = "InspectionCondition"
					, VehicleCondition = "VehicleCondition"
					, Grade = "Grade"
					, AbortInspectionNotes = "AbortInspectionNotes"
					, Location = "Location"
			});
		
		}

	}
}

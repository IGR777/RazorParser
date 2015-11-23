using System;

namespace NewTests
{
	public static class BigTestTextFile
	{
		public static string GetText(){
			return @"  
					<tr>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr.Location</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr.TyreWheelType</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr.Depth</p></td>
                        <td class=""tr1 td7""><p class=""p0 ft3"">@tyr.TyreCondition</p></td>
                        <td class=""tr1 td8""><p class=""p0 ft3"">@tyr.TyreSize @tyr.TyreRim @tyr.TyreSpeed</p></td>
                        <td class=""tr1 td9""><p class=""p0 ft3"">@tyr.Brand</p></td>
                    </tr> 


                    <tr>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr0.Location</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr0.TyreWheelType</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr0.Depth</p></td>
                        <td class=""tr1 td7""><p class=""p0 ft3"">@tyr0.TyreCondition</p></td>
                        <td class=""tr1 td8""><p class=""p0 ft3"">@tyr0.TyreSize @tyr0.TyreRim @tyr0.TyreSpeed</p></td>
                        <td class=""tr1 td9""><p class=""p0 ft3"">@tyr0.Brand</p></td>
                    </tr> 


                    <tr>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr1.Location</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr1.TyreWheelType</p></td>
                        <td class=""tr1 td0""><p class=""p0 ft3"">@tyr1.Depth</p></td>
                        <td class=""tr1 td7""><p class=""p0 ft3"">@tyr1.TyreCondition</p></td>
                        <td class=""tr1 td8""><p class=""p0 ft3"">@tyr1.TyreSize @tyr1.TyreRim @tyr1.TyreSpeed</p></td>
                        <td class=""tr1 td9""><p class=""p0 ft3"">@tyr1.Brand</p></td>
                    </tr>"
				.Trim();
		}
	}
}


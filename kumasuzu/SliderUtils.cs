using System;
using System.Collections.Generic;

namespace kumasuzu
{
	public class SliderUtils
	{
		private static List<int> CONVERTER = new List<int>()
		{
			1,2,3,4,5,7,10,15,20,30,60
		};

		private SliderUtils()
		{			
		}

		public static double convertToValue(int display)
		{
			return CONVERTER.IndexOf(display);
		}

		public static double convertToDisplay(double value)
		{
			return CONVERTER[(int)value];
		}
	}
}

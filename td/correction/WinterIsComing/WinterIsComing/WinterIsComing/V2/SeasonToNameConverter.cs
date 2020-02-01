using System;
using System.Globalization;
using Xamarin.Forms;

namespace WinterIsComing.V2
{
	public class SeasonToNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Season season = (Season) value;
			switch (season)
			{
				case Season.Winter:
					return "Winter";
				case Season.Spring:
					return "Spring";
				case Season.Summer:
					return "Summer";
				case Season.Autumn:
					return "Autumn";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
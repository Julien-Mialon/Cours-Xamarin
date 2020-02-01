using System;
using System.Globalization;
using Xamarin.Forms;

namespace WinterIsComing.V2
{
	public class SeasonToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Season season = (Season) value;
			switch (season)
			{
				case Season.Winter:
					return "winter.jpg";
				case Season.Spring:
					return "spring.jpg";
				case Season.Summer:
					return "summer.jpg";
				case Season.Autumn:
					return "autumn.jpg";
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
using System;
using Storm.Mvvm;

namespace Seasons
{
	public class MainViewModel : ViewModelBase
	{
		public static DateTime StartSpring = new DateTime(2021, 3, 20);
		public static DateTime StartSummer = new DateTime(2021, 6, 21);
		public static DateTime StartAutumn = new DateTime(2021, 9, 22);
		public static DateTime StartWinter = new DateTime(2021, 12, 21);

		private string _season;
		private string _imageSource;
		private DateTime _date;

		public string Season
		{
			get => _season;
			set => SetProperty(ref _season, value);
		}

		public string ImageSource
		{
			get => _imageSource;
			set => SetProperty(ref _imageSource, value);
		}

		public DateTime Date
		{
			get => _date;
			set
			{
				SetProperty(ref _date, value);
				UpdateDate();
			}
		}

		private void UpdateDate()
		{
			if (Date < StartSpring)
			{
				ImageSource = "winter";
				Season = "Hiver";
			}
			else if (Date < StartSummer)
			{
				ImageSource = "spring";
				Season = "Printemps";
			}
			else if (Date < StartAutumn)
			{
				ImageSource = "summer";
				Season = "Eté";
			}
			else if (Date < StartWinter)
			{
				ImageSource = "autumn";
				Season = "Automne";
			}
			else
			{
				ImageSource = "winter";
				Season = "Hiver";
			}
		}
	}
}

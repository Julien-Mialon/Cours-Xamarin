using System;
using Storm.Mvvm;

namespace WinterIsComing.V1
{
	public class SeasonViewModel : ViewModelBase
	{
		// les dates des saisons pour 2020
		private static readonly DateTime WinterEnd = new DateTime(2020, 3, 20);
		private static readonly DateTime SpringEnd = new DateTime(2020, 6, 20);
		private static readonly DateTime SummerEnd = new DateTime(2020, 9, 22);
		private static readonly DateTime AutumnEnd = new DateTime(2020, 12, 21);
		
		// champs privés de la classe pour stocker les valeurs des propriétés pour le binding
		private DateTime _selectedDate = DateTime.Now;
		private string _seasonName;
		private string _seasonImage;
		
		// propriétés pour le binding
		public string SeasonName
		{
			get => _seasonName;
			set => SetProperty(ref _seasonName, value);
		}

		public string SeasonImage
		{
			get => _seasonImage;
			set => SetProperty(ref _seasonImage, value);
		}

		public DateTime SelectedDate
		{
			get => _selectedDate;
			set
			{
				if (SetProperty(ref _selectedDate, value))
				{
					OnDateChanged(value);
				}
			}
		}

		public SeasonViewModel()
		{
			// initialisation de la valeur
			OnDateChanged(SelectedDate);
		}

		// Méthode appelé au changement de la date
		private void OnDateChanged(DateTime date)
		{
			if (date < WinterEnd || date >= AutumnEnd)
			{
				SeasonName = "Winter";
				SeasonImage = "winter.jpg";
			}
			else if (date < SpringEnd)
			{
				SeasonName = "Spring";
				SeasonImage = "spring.jpg";
			}
			else if (date < SummerEnd)
			{
				SeasonName = "Summer";
				SeasonImage = "summer.jpg";
			}
			else if (date < AutumnEnd)
			{
				SeasonName = "Autumn";
				SeasonImage = "autumn.jpg";
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(date), date, "Expecting date in 2020");
			}
		}
	}
}
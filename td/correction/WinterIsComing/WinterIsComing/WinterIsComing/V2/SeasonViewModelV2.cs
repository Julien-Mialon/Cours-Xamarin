using System;
using System.Threading.Tasks;
using Storm.Mvvm;

namespace WinterIsComing.V2
{
	public enum Season
	{
		Winter,
		Spring,
		Summer,
		Autumn
	}
	
	public class SeasonViewModelV2 : ViewModelBase
	{
		// les dates des saisons pour 2020
		private static readonly DateTime WinterEnd = new DateTime(2020, 3, 20);
		private static readonly DateTime SpringEnd = new DateTime(2020, 6, 20);
		private static readonly DateTime SummerEnd = new DateTime(2020, 9, 22);
		private static readonly DateTime AutumnEnd = new DateTime(2020, 12, 21);
		
		// champs privés de la classe pour stocker les valeurs des propriétés pour le binding
		private DateTime _selectedDate = DateTime.Now;
		private Season _season;
		
		// propriétés pour le binding
		public Season Season
		{
			get => _season;
			set => SetProperty(ref _season, value);
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

		public override Task OnResume()
		{
			// initialisation de la valeur
			OnDateChanged(SelectedDate);
			
			return base.OnResume();
		}

		// Méthode appelé au changement de la date
		private void OnDateChanged(DateTime date)
		{
			if (date < WinterEnd || date >= AutumnEnd)
			{
				Season = Season.Winter;
			}
			else if (date < SpringEnd)
			{
				Season = Season.Spring;
			}
			else if (date < SummerEnd)
			{
				Season = Season.Summer;
			}
			else if (date < AutumnEnd)
			{
				Season = Season.Autumn;
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(date), date, "Expecting date in 2020");
			}
		}
	}
}
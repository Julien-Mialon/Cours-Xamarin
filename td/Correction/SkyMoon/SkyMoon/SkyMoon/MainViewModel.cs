using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Forms;

namespace SkyMoon
{
	public class MainViewModel : ViewModelBase
	{
		public ICommand NextCommand { get;  }

		public MainViewModel()
		{
			NextCommand = new Command(NextAction);
		}

		private void NextAction()
		{
			INavigationService navigationService = DependencyService.Get<INavigationService>();
			navigationService.PushAsync<SecondPage>();
		}
	}
}

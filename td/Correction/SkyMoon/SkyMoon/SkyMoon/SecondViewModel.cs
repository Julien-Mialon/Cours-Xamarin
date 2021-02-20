using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using Xamarin.Forms;

namespace SkyMoon
{
	class SecondViewModel : ViewModelBase

	{
		public ICommand BackCommand { get; }

		public SecondViewModel()
		{
			BackCommand = new Command(BackAction);
		}

		private void BackAction()
		{
			INavigationService navigationService = DependencyService.Get<INavigationService>();
			navigationService.PopAsync();
		}
	}
}

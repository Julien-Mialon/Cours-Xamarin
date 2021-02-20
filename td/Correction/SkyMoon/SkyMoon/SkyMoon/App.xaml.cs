using System;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkyMoon
{
	public partial class App : MvvmApplication
	{
		public App() : base(() => new MainPage())
		{
			InitializeComponent();
		}
	}
}

﻿using System;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Seasons
{
	public partial class App : MvvmApplication
	{
		public App() : base(() => new MainPage())
		{
			InitializeComponent();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}

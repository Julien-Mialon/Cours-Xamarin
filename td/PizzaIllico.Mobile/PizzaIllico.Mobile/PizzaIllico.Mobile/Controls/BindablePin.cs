using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PizzaIllico.Mobile.Controls
{
	public class BindablePin : Pin
	{
		public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(BindablePin));

		public ICommand Command
		{
			get => (ICommand) GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		public BindablePin()
		{
			InfoWindowClicked += OnInfoWindowClicked;
		}

		private void OnInfoWindowClicked(object sender, PinClickedEventArgs e)
		{
			Command?.Execute(null);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WinterIsComing.V1
{
	public partial class SeasonPage : ContentPage
	{
		public SeasonPage()
		{
			InitializeComponent();
			BindingContext = new SeasonViewModel();
		}
	}
}
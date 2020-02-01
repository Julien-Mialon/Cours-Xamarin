using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace WinterIsComing
{
	public partial class App
	{
		public App() 
			: base(() => new V1.SeasonPage())
			//: base(() => new V2.SeasonPageV2())
		{
			InitializeComponent();
		}
	}
}
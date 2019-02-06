using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LiveCoding2
{
    public partial class App : Application
    {
        public App() // :base(() => new HomePage())
        {
            InitializeComponent();

            MainPage = new HomePage();
        }
    }
}

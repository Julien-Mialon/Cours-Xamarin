using Storm.Mvvm;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TimeTracker.Apps
{
    public partial class App : MvvmApplication
    {
        public App() : base(CreateStartPage)
        {
            InitializeComponent();
            
            DependencyService.Register<TodoService>();
        }

        private static Page CreateStartPage()
        {
            return new MainPage();
        }
    }
}
using TimeTracker.Apps.ViewModels;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Apps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditPage
    {
        public AddOrEditPage()
        {
            InitializeComponent();
            BindingContext = new AddOrEditViewModel();
        }
    }
}
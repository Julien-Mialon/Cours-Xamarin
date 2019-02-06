using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LiveCoding2.Models;
using Storm.Mvvm;

namespace LiveCoding2.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _title;
        private ObservableCollection<Todo> _todos;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ObservableCollection<Todo> Todos
        {
            get => _todos;
            set => SetProperty(ref _todos, value);
        }

        public HomeViewModel()
        {
            Title = "Choses à faire";
        }

        public override async Task OnResume()
        {
            await base.OnResume();

            Todos = Store.Todos;
        }
    }
}

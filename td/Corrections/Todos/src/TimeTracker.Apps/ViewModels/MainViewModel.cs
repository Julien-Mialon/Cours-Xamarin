using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using TimeTracker.Apps.Pages;
using TimeTracker.Apps.Services;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Todo> _todos;

        public ObservableCollection<Todo> Todos
        {
            get => _todos;
            set => SetProperty(ref _todos, value);
        }
        
        public ICommand AddCommand { get; }

        public MainViewModel()
        {
            Todos = new ObservableCollection<Todo>();
            
            
            AddCommand = new Command(AddAction);
        }

        public override Task OnResume()
        {
            Todos.Clear();
            List<string> items = DependencyService.Get<TodoService>().Todos;
            foreach (string item in items)
            {
                Todos.Add(Create(item));
            }
            
            return base.OnResume();
        }

        private Todo Create(string text)
        {
            return new Todo(
                new Command<Todo>(DeleteAction),
                new Command<Todo>(EditAction)
            )
            {
                Text = text
            };
        }

        private void EditAction(Todo todo)
        {
            int index = Todos.IndexOf(todo);

            NavigationService.PushAsync<AddOrEditPage>(new Dictionary<string, object>()
            {
                ["Index"] = index
            });
        }

        private void DeleteAction(Todo todo)
        {
            int index = Todos.IndexOf(todo);
            var todoService = DependencyService.Get<TodoService>();
            List<string> items = todoService.Todos;
            items.RemoveAt(index);
            Todos.RemoveAt(index);
            todoService.Save();
        }

        private void AddAction()
        {
            NavigationService.PushAsync<AddOrEditPage>(new Dictionary<string, object>()
            {
                ["Index"] = -1
            });
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using TimeTracker.Apps.Services;
using Xamarin.Forms;

namespace TimeTracker.Apps.ViewModels
{
    public class AddOrEditViewModel : ViewModelBase
    {
        private string _text;
        private int _index;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        [NavigationParameter]
        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }

        public ICommand CancelCommand { get; }
        
        public ICommand ValidateCommand { get; }

        public AddOrEditViewModel()
        {
            ValidateCommand = new Command(ValidateAction);
            CancelCommand = new Command(CancelAction);
        }

        public override Task OnResume()
        {
            if (Index >= 0)
            {
                var todoService = DependencyService.Get<TodoService>();
                Text = todoService.Todos[Index];
            }
            return base.OnResume();
        }

        private async void CancelAction()
        {
            await NavigationService.PopAsync();
        }

        private async void ValidateAction()
        {
            string text = Text;
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            var todoService = DependencyService.Get<TodoService>();
            List<string> todos = todoService.Todos;
            if (Index < 0)
            {
                todos.Add(text);
            }
            else
            {
                todos[Index] = text;
            }
            todoService.Save();

            Text = "";
            await NavigationService.PopAsync();
        }
    }
}
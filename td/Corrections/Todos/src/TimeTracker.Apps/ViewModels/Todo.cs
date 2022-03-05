using System.Windows.Input;
using Storm.Mvvm;

namespace TimeTracker.Apps.ViewModels
{
    public class Todo : NotifierBase
    {
        private string _text;

        public Todo(ICommand deleteCommand, ICommand editCommand)
        {
            DeleteCommand = deleteCommand;
            EditCommand = editCommand;
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public ICommand DeleteCommand { get; }
        
        public ICommand EditCommand { get; }
    }
}
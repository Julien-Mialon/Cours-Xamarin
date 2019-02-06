using LiveCoding1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LiveCoding1
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            TodoList.ItemsSource = Store.Todos;
        }

        private void DeleteButtonClicked(object sender, System.EventArgs e)
        {
            if(sender is Button button && button.BindingContext is Todo todo)
            {
                Store.Todos.Remove(todo);
            }
        }
    }
}

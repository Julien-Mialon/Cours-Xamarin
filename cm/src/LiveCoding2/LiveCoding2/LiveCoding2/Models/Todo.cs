using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LiveCoding2.Models
{
    public class Todo
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public ICommand DeleteCommand { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
            DeleteCommand = new Command(DeleteAction);
        }

        public Todo(string text) : this()
        {
            Text = text;
        }


        private void DeleteAction(object _)
        {
            Store.Todos.Remove(this);
        }
    }
}

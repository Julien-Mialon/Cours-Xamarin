using System;

namespace LiveCoding1.Models
{
    public class Todo
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        public Todo()
        {

        }

        public Todo(string text)
        {
            Text = text;
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}

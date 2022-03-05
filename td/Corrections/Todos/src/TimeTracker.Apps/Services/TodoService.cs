using System.Collections.Generic;
using Newtonsoft.Json;
using TimeTracker.Apps.ViewModels;
using Xamarin.Essentials;

namespace TimeTracker.Apps.Services
{
    public class TodoService
    {
        public List<string> Todos { get; }

        public TodoService()
        {
            string todos = Preferences.Get(nameof(TodoService), "[]");
            Todos = JsonConvert.DeserializeObject<List<string>>(todos);
        }

        public void Save()
        {
            string content = JsonConvert.SerializeObject(Todos);
            Preferences.Set(nameof(TodoService), content);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LiveCoding2.Models
{
    public static class Store
    {
        public static ObservableCollection<Todo> Todos { get; }

        static Store()
        {
            Todos = new ObservableCollection<Todo>()
            {
                new Todo("Faire un plan du cours"),
                new Todo("Préparer le powerpoint"),
                new Todo("Préparer le live coding #1"),
                new Todo("Préparer le live coding #2"),
                new Todo("Écrire le sujet de TD"),
                new Todo("Faire le TD pour fournir une correction si les étudiants sont sages"),
                new Todo("Aller à Orléans"),
                new Todo("Ne pas oublier son PC"),
                new Todo("Se lever pour aller faire le cours à 9h"),
                new Todo("Prendre un café !"),
            };
        }
    }
}

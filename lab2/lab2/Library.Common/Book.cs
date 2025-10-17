using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Book : Item
    {
        public string Author { get; set; }
        public int Pages { get; set; }


        public Book(string title, int year, string author, int pages)
            : base(title, year)
        {
            Author = author;
            Pages = pages;
        }

        
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Автор: {Author}, Сторінок: {Pages}");
        }
    }
}

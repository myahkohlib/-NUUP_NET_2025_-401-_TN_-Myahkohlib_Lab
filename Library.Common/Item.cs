using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Item
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; set; }
        public int Year { get; set; }
    
      public static int ItemCount { get; private set; }

        public Item(string title, int year)
        {
            Title = title;
            Year = year;
            ItemCount++;
        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Назва: {Title}, Рік: {Year}, GUID: {Id}");
        }
    }
}

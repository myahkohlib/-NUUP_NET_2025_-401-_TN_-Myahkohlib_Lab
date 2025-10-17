using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Reader
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Age { get; set; }

     
        public Reader(string name, int age)
        {
            Name = name;
            Age = age;
        }

      
        public void DisplayInfo()
        {
            Console.WriteLine($"Читач: {Name}, Вік: {Age}, GUID: {Id}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Library.Common
{
    internal class Bus
    {
        public Guid Id { get; set; }
        public int Seats { get; set; }
        public double Speed { get; set; }

        public static Bus CreateNew()
        {
            var rnd = new Random();
            return new Bus
            {
                Id = Guid.NewGuid(),
                Seats = rnd.Next(20, 60),
                Speed = rnd.Next(60, 150)
            };
        }
    }
}


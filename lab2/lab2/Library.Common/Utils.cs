using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public static class Utils
    {

        public static void Print<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                if (item is Item libItem)
                {
                    libItem.DisplayInfo();
                }
                else if (item is Reader reader)
                {
                    reader.DisplayInfo();
                }
                else
                {
                    Console.WriteLine(item?.ToString() ?? "NULL");
                }
            }
        }
    }

}

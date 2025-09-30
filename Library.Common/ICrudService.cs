using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal interface ICrudService<T> where T : class
    {
        void Create(T element);
        T Read(Guid id);
        IEnumerable<T> ReadAll();
        void Update(T element);
        void Remove(Guid id);
    }
}

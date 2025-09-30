using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly Dictionary<Guid, T> _dataStore = new Dictionary<Guid, T>();

        public void Create(T element)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Тип T повинен мати публічну властивість 'Id' типу Guid.");

            var idValue = idProperty.GetValue(element);
            if (idValue == null)
                throw new InvalidOperationException("Значення властивості 'Id' не може бути null.");

            var id = (Guid)idValue;
            if (!_dataStore.ContainsKey(id))
            {
                _dataStore.Add(id, element);
                Console.WriteLine("Елемент додано.");
            }
            else
            {
                Console.WriteLine("Елемент уже існує.");
            }
        }

        public T Read(Guid id)
        {
            _dataStore.TryGetValue(id, out T element);
            return element;
        }

        public IEnumerable<T> ReadAll() => _dataStore.Values;

        public void Update(T element)
        {
            var idProperty = typeof(T).GetProperty("Id");
            var id = (Guid)idProperty.GetValue(element);

            if (_dataStore.ContainsKey(id))
            {
                _dataStore[id] = element;
                Console.WriteLine("Елемент оновлено.");
            }
            else
            {
                Console.WriteLine("Елемент не знайдено.");
            }
        }

        public void Remove(Guid id)
        {
            if (_dataStore.Remove(id))
                Console.WriteLine("Елемент видалено.");
            else
                Console.WriteLine("Елемент не знайдено.");
        }
    }
}

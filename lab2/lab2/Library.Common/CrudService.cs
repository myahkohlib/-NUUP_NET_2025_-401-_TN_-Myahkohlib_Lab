using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace lab2
{
    internal class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly Dictionary<Guid, T> _dataStore = new Dictionary<Guid, T>();
        private readonly PropertyInfo _idProperty;

        public CrudService()
        {
            _idProperty = typeof(T).GetProperty("Id");
            if (_idProperty == null || _idProperty.PropertyType != typeof(Guid))
                throw new InvalidOperationException("Тип T повинен мати публічну властивість 'Id' типу Guid.");
        }

        public void Create(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var id = GetId(element);

            if (!_dataStore.ContainsKey(id))
            {
                _dataStore.Add(id, element);
                Console.WriteLine("Елемент додано.");
            }
            else
            {
                Console.WriteLine("Елемент вже існує.");
            }
        }

        public T Read(Guid id)
        {
            _dataStore.TryGetValue(id, out T element);
            return element;
        }

        public IEnumerable<T> ReadAll() => _dataStore.Values.ToList();

        public void Update(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var id = GetId(element);

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

        private Guid GetId(T element)
        {
            var idValue = _idProperty.GetValue(element);
            if (idValue == null)
                throw new InvalidOperationException("Значення властивості 'Id' не може бути null.");

            return (Guid)idValue;
        }
    }

    internal interface ICrudService<T> where T : class
    {
    }
}
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _dataStore = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        public string FilePath { get; }

        public CrudServiceAsync(string filePath)
        {
            FilePath = filePath;
        }

        public async Task<bool> CreateAsync(T element)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Тип T повинен мати властивість 'Id' типу Guid.");

            var id = (Guid)idProperty.GetValue(element);
            await _semaphore.WaitAsync();
            try
            {
                return _dataStore.TryAdd(id, element);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<T> ReadAsync(Guid id)
        {
            await Task.Yield();
            _dataStore.TryGetValue(id, out var element);
            return element;
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            await Task.Yield();
            return _dataStore.Values.ToList();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            await Task.Yield();
            return _dataStore.Values
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToList();
        }

        public async Task<bool> UpdateAsync(T element)
        {
            var id = (Guid)typeof(T).GetProperty("Id")!.GetValue(element);
            await _semaphore.WaitAsync();
            try
            {
                if (_dataStore.ContainsKey(id))
                {
                    _dataStore[id] = element;
                    return true;
                }
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> RemoveAsync(T element)
        {
            var id = (Guid)typeof(T).GetProperty("Id")!.GetValue(element);
            await _semaphore.WaitAsync();
            try
            {
                return _dataStore.TryRemove(id, out _);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var json = JsonSerializer.Serialize(_dataStore.Values, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath, json);
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator() => _dataStore.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
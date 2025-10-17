using lab2;
using lab2.Library.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("🚌 Демонстрація асинхронного CRUD-сервісу з моделлю 'Bus'");
            Console.WriteLine("---------------------------------------------------------");

            var busService = new CrudServiceAsync<Bus>("buses.json");

            Console.WriteLine("1️⃣ Створення перших об’єктів:");
            var bus1 = Bus.CreateNew();
            var bus2 = Bus.CreateNew();
            await busService.CreateAsync(bus1);
            await busService.CreateAsync(bus2);

            (await busService.ReadAllAsync()).ToList().ForEach(b =>
                Console.WriteLine($"🚌 Bus: {b.Id}, Seats={b.Seats}, Speed={b.Speed}"));
            Console.WriteLine("---------------------------------------------------------");

            Console.WriteLine("2️⃣ Читання автобуса за ID:");
            var foundBus = await busService.ReadAsync(bus1.Id);
            Console.WriteLine($"Знайдено: {foundBus.Id}, Seats={foundBus.Seats}, Speed={foundBus.Speed}");
            Console.WriteLine("---------------------------------------------------------");

            Console.WriteLine("3️⃣ Оновлення автобуса:");
            bus1.Speed += 15;
            await busService.UpdateAsync(bus1);
            Console.WriteLine($"Bus {bus1.Id} оновлено. Нова швидкість: {bus1.Speed}");
            Console.WriteLine("---------------------------------------------------------");

            Console.WriteLine("4️⃣ Видалення автобуса:");
            await busService.RemoveAsync(bus2);
            Console.WriteLine("Після видалення:");
            (await busService.ReadAllAsync()).ToList().ForEach(b =>
                Console.WriteLine($"🚌 Bus: {b.Id}, Seats={b.Seats}, Speed={b.Speed}"));
            Console.WriteLine("---------------------------------------------------------");

            Console.WriteLine("5️⃣ Паралельне створення ще 1000 автобусів:");
            var semaphore = new SemaphoreSlim(10);
            var resetEvent = new AutoResetEvent(false);

            await Task.Run(() =>
            {
                Parallel.For(0, 1000, async i =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        await busService.CreateAsync(Bus.CreateNew());
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
                resetEvent.Set();
            });
            resetEvent.WaitOne();

            var all = await busService.ReadAllAsync();
            var min = all.Min(b => b.Speed);
            var max = all.Max(b => b.Speed);
            var avg = all.Average(b => b.Speed);

            Console.WriteLine($"✅ Створено всього: {all.Count()} автобусів.");
            Console.WriteLine($"Мінімальна швидкість: {min}");
            Console.WriteLine($"Максимальна швидкість: {max}");
            Console.WriteLine($"Середня швидкість: {avg:F2}");
            Console.WriteLine("---------------------------------------------------------");

            await busService.SaveAsync();
            Console.WriteLine("💾 Колекцію збережено у файл 'buses.json'.");
            Console.WriteLine("✅ Демонстрацію завершено.");
        }
    }
}

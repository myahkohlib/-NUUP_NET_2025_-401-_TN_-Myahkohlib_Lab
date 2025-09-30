using Lab1;
using System;
using System.Reflection.PortableExecutable;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("📚 Демонстрація роботи CRUD-сервісу з моделлю 'Бібліотека'");
        Console.WriteLine("-------------------------------------------------");

        var bookService = new CrudService<Book>();
        var magazineService = new CrudService<Magazine>();
        var readerService = new CrudService<Reader>();

        var book1 = new Book("Пригоди Шерлока Холмса", 1892, "Артур Конан Дойл", 320);
        var book2 = new Book("Фундація", 1951, "Айзек Азімов", 255);

        var mag1 = new Magazine("Наука і Життя", 2023, "Наукове видавництво", 5);

        var reader1 = new Reader("Іван Петренко", 22);

        bookService.Create(book1);
        bookService.Create(book2);
        magazineService.Create(mag1);
        readerService.Create(reader1);

        Console.WriteLine("1. Об’єкти створено:");
        bookService.ReadAll().Print();
        magazineService.ReadAll().Print();
        readerService.ReadAll().Print();
        Console.WriteLine("-------------------------------------------------");

  
        Console.WriteLine("2. Читання книги за ID:");
        var foundBook = bookService.Read(book2.Id);
        foundBook?.DisplayInfo();
        Console.WriteLine("-------------------------------------------------");

        Console.WriteLine("3. Оновлення книги:");
        book1.Title = "Пригоди Шерлока Холмса (оновлене видання)";
        bookService.Update(book1);
        bookService.ReadAll().Print();
        Console.WriteLine("-------------------------------------------------");

      
        Console.WriteLine("4. Видалення журналу:");
        magazineService.Remove(mag1.Id);
        magazineService.ReadAll().Print();
        Console.WriteLine("-------------------------------------------------");

        Console.WriteLine("Демонстрацію завершено.");
        Console.ReadKey();
    }
}
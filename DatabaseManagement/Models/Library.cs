using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseManagement.Models
{
    public class Library
    {
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public void LibraryMenu(int currentUserId) 
        {
            UserManager userManager = new UserManager();
            Console.WriteLine("\nБиблиотека:");
            Console.WriteLine("1. Посмотреть список игр");
            Console.WriteLine("2. Удалить игру из библиотеки");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewGamesInLibrary(currentUserId);
                    break;
                case "2":
                    DeleteGameFromLibrary();
                    break;
                case "3":
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void ViewGamesInLibrary(int currentUserId) 
        {
            Console.WriteLine("\nТип:");
            Console.WriteLine("1. По убыванию");
            Console.WriteLine("2. По возрастанию");
            Console.WriteLine("3. По жанру");
            Console.WriteLine("4. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Descending(currentUserId);
                    LibraryMenu(currentUserId);
                    break;
                case "2":
                    Ascending(currentUserId);
                    LibraryMenu(currentUserId);
                    break;
                case "3":
                    WithGenres(currentUserId);
                    LibraryMenu(currentUserId);
                    break;
                case "4":
                    LibraryMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void Descending(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var library = context.Libraries
                    .Include(l => l.Game)
                    .Where(l => l.UserId == currentUserId)
                    .OrderByDescending(l => l.Game.ReleaseDate)
                    .ToList();

                if (!library.Any())
                {
                    Console.WriteLine("\nВаша библиотека пуста.");
                    return;
                }

                Console.WriteLine($"\nВывод игр по убыванию: ");
                foreach (var item in library)
                {
                    Console.WriteLine($"Игра: {item.Game.GameName}");
                    Console.WriteLine($"Платформа: {item.Game.GamePlatform.PlatformName}");
                    Console.WriteLine($"Цена: {item.Game.Price} | Дата выхода: {item.Game.ReleaseDate.ToShortDateString()}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
        public void Ascending(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var library = context.Libraries
                    .Include(l => l.Game)
                    .Where(l => l.UserId == currentUserId)
                    .OrderBy(l => l.Game.ReleaseDate)
                    .ToList();

                if (!library.Any())
                {
                    Console.WriteLine("\nВаша библиотека пуста.");
                    return;
                }

                Console.WriteLine($"\nВывод игр по возрастанию: ");
                foreach (var item in library)
                {
                    Console.WriteLine($"Игра: {item.Game.GameName}");
                    Console.WriteLine($"Платформа: {item.Game.GamePlatform.PlatformName}");
                    Console.WriteLine($"Цена: {item.Game.Price} | Дата выхода: {item.Game.ReleaseDate.ToShortDateString()}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
        public void WithGenres(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var library = context.Libraries
                    .Include(l => l.Game).Include("Game.Genre")
                    .Where(l => l.UserId == currentUserId)
                    .OrderBy(l => l.Game.Genre.GenreName)
                    .ToList();

                if (!library.Any())
                {
                    Console.WriteLine("\nВаша библиотека пуста.");
                    return;
                }

                Console.WriteLine($"\nВывод игр по жанрам: ");
                foreach (var item in library)
                {
                    Console.WriteLine($"Игра: {item.Game.GameName}");
                    Console.WriteLine($"Жанр: {item.Game.Genre.GenreName}");
                    Console.WriteLine($"Цена: {item.Game.Price} | Дата выхода: {item.Game.ReleaseDate.ToShortDateString()}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
        public void DeleteGameFromLibrary()
        {

        }
    }
}
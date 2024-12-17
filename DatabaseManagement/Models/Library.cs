using Spectre.Console;
using System;
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
            //Заголовок с рамкой и цветом
            AnsiConsole.Write(
                new FigletText("Library")
                    .Color(Color.Yellow));

            //Меню выбора действий
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Выберите действие в библиотеке:[/]")
                    .AddChoices(new[]
                    {
                "1. Посмотреть список игр",
                "2. Удалить игру из библиотеки",
                "3. Назад"
                    })
                    .HighlightStyle("green"));

            //Обработка выбора пользователя
            switch (choice)
            {
                case "1. Посмотреть список игр":
                    ViewGamesInLibrary(currentUserId);
                    break;

                case "2. Удалить игру из библиотеки":
                    DeleteGameFromLibrary(currentUserId);
                    LibraryMenu(currentUserId);
                    break;

                case "3. Назад":
                    var userManager = new UserManager();
                    userManager.ShowProfileMenu(currentUserId);
                    break;

                default:
                    AnsiConsole.MarkupLine("[bold red]Неверный выбор, попробуйте снова.[/]");
                    LibraryMenu(currentUserId);
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
                    ViewGamesInLibrary(currentUserId);
                    break;
            }
        }

        public void Descending(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var library = context.Libraries
                    .Include(l => l.Game)
                    .Include(l => l.Game.GamePlatform)
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
                    .Include(l => l.Game.GamePlatform)
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

        public void DeleteGameFromLibrary(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, которую хотите удалить из библиотеки: ");
                string gameName = Console.ReadLine();

                var libraryItem = context.Libraries
                    .Include(l => l.Game)
                    .FirstOrDefault(l => l.UserId == currentUserId && l.Game.GameName == gameName);

                if (libraryItem == null)
                {
                    Console.WriteLine("Ошибка: Игра не найдена в вашей библиотеке.");
                    return;
                }

                context.Libraries.Remove(libraryItem);
                context.SaveChanges();

                Console.WriteLine($"Игра '{gameName}' успешно удалена из вашей библиотеки.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace DatabaseManagement.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int GamePlatformId {  get; set; }
        public GamePlatform GamePlatform { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public void GameMenu() 
        {
            Review review = new Review();
            UserManager userManager = new UserManager();
            Console.WriteLine("\nИгры:");
            Console.WriteLine("1. Посмотреть список игр");
            Console.WriteLine("2. Добавить игру в библиотеку");
            Console.WriteLine("3. Отзывы");
            Console.WriteLine("4. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewGames();
                    break;
                case "2":
                    AddGameInLibrary();
                    break;
                case "3":
                    review.ReviewMenu();
                    break;
                case "4":
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void ViewGames() 
        {
            Review review = new Review();
            UserManager userManager = new UserManager();
            Console.WriteLine("\nТип:");
            Console.WriteLine("1. По жанру");
            Console.WriteLine("2. По цене");
            Console.WriteLine("3. По имени");
            Console.WriteLine("4. По количеству");
            Console.WriteLine("5. По отзывам");
            Console.WriteLine("6. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ByGenre();
                    break;
                case "2":
                    ByPrice();
                    break;
                case "3":
                    ByName();
                    break;
                case "4":
                    ByQuantity();
                    break;
                case "5":
                    ByReviews();
                    break;
                case "6":
                    GameMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void ByReviews()
        {
            
            Console.WriteLine("\nТип:");
            Console.WriteLine("1. Вывести по положительным отзывам");
            Console.WriteLine("2. Вывести по негативным отзывам");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    OutputReviewsByPositive();
                    break;
                case "2":
                    OutputReviewsByNegative();
                    break;
                case "3":
                    GameMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }

        public void ByGenre()
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите жанр для поиска игр: ");
                string genre = Console.ReadLine();

                var games = context.Games
                .Where(g => g.Genre.GenreName == genre) 
                .ToList();

                if (!games.Any())
                {
                    Console.WriteLine($"Игры с жанром '{genre}' не найдены.");
                    return;
                }

                Console.WriteLine($"Список игр в жанре '{genre}': ");
                foreach (var game in games)
                {
                    Console.WriteLine($"- {game.GameName} | Цена: {game.Price} | Дата выхода: {game.ReleaseDate.ToShortDateString()}");
                }
            } 
        }
        public void ByPrice()
        {
            
        }
        public void ByName()
        {

        }
        public void ByQuantity()
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите количество игр, которые надо вывести: ");
                int count = Convert.ToInt32(Console.ReadLine());

                var games = context.Games
                .Include(g => g.Genre) // Загружаем данные о жанре
                .OrderByDescending(g => g.ReleaseDate) //Сортировка по дате выпуска, новые игры первыми
                .Take(count) //Ограничение количества
                .ToList();

                if (!games.Any())
                {
                    Console.WriteLine("Список игр пуст.");
                    return;
                }

                Console.WriteLine($"Топ-{count} игр, которые актуальны по дате:");
                foreach (var game in games)
                {
                    Console.WriteLine($"- {game.GameName} | Жанр: {game.Genre.GenreName} | Цена: {game.Price} | Дата выхода: {game.ReleaseDate.ToShortDateString()}");
                }
            }
        }

        public void OutputReviewsByPositive()
        {

        }
        public void OutputReviewsByNegative()
        {

        }

        public void AddGameInLibrary() 
        {

        }
    }
}
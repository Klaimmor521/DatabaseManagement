using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
        private int _currentUserId; //Поле для хранения ID текущего пользователя

        //Конструктор с передачей currentUserId
        public Game(int currentUserId)
        {
            _currentUserId = currentUserId;
        }
        public Game() { }
        public void GameMenu() 
        {
            Review review = new Review();
            UserManager userManager = new UserManager();
            Wishlist wishlist = new Wishlist();
            Console.WriteLine("\nИгры:");
            Console.WriteLine("1. Посмотреть список игр");
            Console.WriteLine("2. Добавить игру в библиотеку");
            Console.WriteLine("3. Добавить игру в желаемые");
            Console.WriteLine("4. Отзывы");
            Console.WriteLine("5. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewGames();
                    break;
                case "2":
                    AddGameInLibrary();
                    GameMenu();
                    break;
                case "3":
                    wishlist.AddToWishlist(_currentUserId);
                    GameMenu();
                    break;
                case "4":
                    userManager.ReviewMenu();
                    GameMenu();
                    break;
                case "5":
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
                    GameMenu();
                    break;
                case "2":
                    ByPrice();
                    GameMenu();
                    break;
                case "3":
                    ByName();
                    GameMenu();
                    break;
                case "4":
                    ByQuantity();
                    GameMenu();
                    break;
                case "5":
                    ByReviews();
                    GameMenu();
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
                    GameMenu();
                    break;
                case "2":
                    OutputReviewsByNegative();
                    GameMenu();
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
                .Include(g => g.Genre) 
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
            using (var context = new GameLibraryContext())
            {
                var positiveReviews = context.Reviews
                    .Include(u => u.User)       
                    .Include(g => g.Game)       
                    .Where(r => r.Rating >= 7) //Фильтр для положительных отзывов
                    .ToList();

                if (!positiveReviews.Any())
                {
                    Console.WriteLine($"Положительная отзывы отсутствуют.");
                    return;
                }

                Console.WriteLine($"Положительные отзывы: ");
                foreach (var review in positiveReviews)
                {
                    Console.WriteLine($"Игра: {review.Game.GameName}");
                    Console.WriteLine($"Автор: {review.User.Nickname}");
                    Console.WriteLine($"Рейтинг: {review.Rating} / 10");
                    Console.WriteLine($"Отзыв: {review.ReviewText}");
                    Console.WriteLine($"Дата: {review.CreatedAt.ToShortDateString()}");
                    Console.WriteLine("-----------------------------------");
                }
            }
        }
        public void OutputReviewsByNegative()
        {
            using (var context = new GameLibraryContext())
            {
                var negativeReviews = context.Reviews
                    .Include(u => u.User)
                    .Include(g => g.Game)
                    .Where(r => r.Rating < 6) //Фильтр для положительных отзывов
                    .ToList();

                if (!negativeReviews.Any())
                {
                    Console.WriteLine($"Отрицательные отзывы отсутствуют.");
                    return;
                }

                Console.WriteLine($"Отрицательные отзывы: ");
                foreach (var review in negativeReviews)
                {
                    Console.WriteLine($"Игра: {review.Game.GameName}");
                    Console.WriteLine($"Автор: {review.User.Nickname}");
                    Console.WriteLine($"Рейтинг: {review.Rating} / 10");
                    Console.WriteLine($"Отзыв: {review.ReviewText}");
                    Console.WriteLine($"Дата: {review.CreatedAt.ToShortDateString()}");
                    Console.WriteLine("-----------------------------------");
                }
            }
        }

        public void AddGameInLibrary() 
        {

        }
    }
}
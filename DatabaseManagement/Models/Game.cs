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
        public int GamePlatformId { get; set; }
        public GamePlatform GamePlatform { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public void GameMenu(int currentUserId)
        {
            Review review = new Review();
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
                    ViewGames(currentUserId);
                    break;
                case "2":
                    AddGameInLibrary(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "3":
                    wishlist.AddToWishlist(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "4":
                    review.ReviewMenu(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "5":
                    // Возвращаемся к меню профиля
                    UserManager userManager = new UserManager();
                    userManager.ShowProfileMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    GameMenu(currentUserId);
                    break;
            }
        }

        public void ViewGames(int currentUserId)
        {
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
                    ByGenre(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "2":
                    ByPrice(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "3":
                    ByName(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "4":
                    ByQuantity(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "5":
                    ByReviews(currentUserId);
                    GameMenu(currentUserId);
                    break;
                case "6":
                    GameMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    ViewGames(currentUserId);
                    break;
            }
        }

        public void ByReviews(int currentUserId)
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
                    GameMenu(currentUserId);
                    break;
                case "2":
                    OutputReviewsByNegative();
                    GameMenu(currentUserId);
                    break;
                case "3":
                    GameMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    ByReviews(currentUserId);
                    break;
            }
        }

        public void ByGenre(int currentUserId)
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

        public void ByPrice(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите максимальную цену для поиска игр: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
                {
                    Console.WriteLine("Ошибка: Введите корректное числовое значение для цены.");
                    return;
                }

                var games = context.Games
                    .Where(g => g.Price <= maxPrice) //Фильтрация по цене
                    .OrderBy(g => g.Price) //Сортировка по возрастанию цены
                    .ToList();

                if (!games.Any())
                {
                    Console.WriteLine($"Игры с ценой до {maxPrice} не найдены.");
                    return;
                }

                Console.WriteLine($"Список игр с ценой до {maxPrice}: ");
                foreach (var game in games)
                {
                    Console.WriteLine($"- {game.GameName} | Цена: {game.Price} | Дата выхода: {game.ReleaseDate.ToShortDateString()}");
                }
            }
        }

        public void ByName(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры (или его часть) для поиска: ");
                string nameInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nameInput))
                {
                    Console.WriteLine("Ошибка: Название не должно быть пустым.");
                    return;
                }

                var games = context.Games
                    .Where(g => g.GameName.Contains(nameInput)) //Фильтрация по подстроке в названии
                    .OrderBy(g => g.GameName) //Сортировка по имени
                    .ToList();

                if (!games.Any())
                {
                    Console.WriteLine($"Игры с названием, содержащим '{nameInput}', не найдены.");
                    return;
                }

                Console.WriteLine($"Список игр, содержащих '{nameInput}':");
                foreach (var game in games)
                {
                    Console.WriteLine($"- {game.GameName} | Цена: {game.Price} | Дата выхода: {game.ReleaseDate.ToShortDateString()}");
                }
            }
        }

        public void ByQuantity(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите количество игр, которые надо вывести: ");
                int count = Convert.ToInt32(Console.ReadLine());

                var games = context.Games
                .Include(g => g.Genre)
                .OrderByDescending(g => g.ReleaseDate)
                .Take(count)
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
                    .Where(r => r.Rating >= 7)
                    .ToList();

                if (!positiveReviews.Any())
                {
                    Console.WriteLine($"Положительные отзывы отсутствуют.");
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
                    .Where(r => r.Rating < 6)
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

        public void AddGameInLibrary(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, которую хотите добавить в библиотеку: ");
                string gameName = Console.ReadLine();

                var game = context.Games.FirstOrDefault(g => g.GameName == gameName);
                if (game == null)
                {
                    Console.WriteLine("Ошибка: Игра с таким названием не найдена.");
                    return;
                }

                var exists = context.Libraries.Any(l => l.UserId == currentUserId && l.GameId == game.GameId);
                if (exists)
                {
                    Console.WriteLine("Эта игра уже есть в вашей библиотеке! Выбери другую.");
                    return;
                }

                var newLibraryItem = new Library
                {
                    UserId = currentUserId,
                    GameId = game.GameId
                };

                context.Libraries.Add(newLibraryItem);
                context.SaveChanges();

                Console.WriteLine($"Игра '{game.GameName}' успешно добавлена в вашу библиотеку.");
            }
        }
    }
}
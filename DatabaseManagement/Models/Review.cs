using System;
using System.Linq;
using System.Data.Entity;

namespace DatabaseManagement.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public void ReviewMenu(int currentUserId)
        {
            Game game = new Game();
            Console.WriteLine("\nОтзывы:");
            Console.WriteLine("1. Посмотреть отзывы об игре");
            Console.WriteLine("2. Добавить отзыв");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewReviewsAboutThisGame();
                    game.GameMenu(currentUserId);
                    break;
                case "2":
                    AddReview(currentUserId);
                    game.GameMenu(currentUserId);
                    break;
                case "3":
                    game.GameMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    ReviewMenu(currentUserId);
                    break;
            }
        }

        public void AddReview(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, на которую хотите оставить отзыв: ");
                string gameName = Console.ReadLine();

                var game = context.Games.FirstOrDefault(g => g.GameName == gameName);

                if (game == null)
                {
                    Console.WriteLine("Ошибка: Игра с таким названием не найдена.");
                    return;
                }

                var existingReview = context.Reviews
                    .FirstOrDefault(r => r.UserId == currentUserId && r.GameId == game.GameId);

                if (existingReview != null)
                {
                    Console.WriteLine("Вы уже оставляли отзыв для этой игры! Поставь другим играм отзыв");
                    return;
                }

                int rating;
                while (true)
                {
                    Console.Write("Введите оценку (1-10): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out rating) && rating >= 1 && rating <= 10)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Оценка должна быть числом от 1 до 10. Попробуйте снова.");
                    }
                }

                string reviewText;
                while (true)
                {
                    Console.Write("Введите текст отзыва: ");
                    reviewText = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(reviewText))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Текст отзыва не может быть пустым. Попробуйте снова.");
                    }
                }

                var newReview = new Review
                {
                    UserId = currentUserId,
                    GameId = game.GameId,
                    Rating = rating,
                    ReviewText = reviewText,
                    CreatedAt = DateTime.Now
                };

                context.Reviews.Add(newReview);
                context.SaveChanges();

                Console.WriteLine("Отзыв успешно добавлен!");
            }
        }

        public void ViewReviewsAboutThisGame()
        {
            using (var context = new GameLibraryContext())
            {
                Console.WriteLine("Напиши для какой игры надо посмотреть отзывы: ");
                string gameName = Console.ReadLine();
                var reviews = context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Game)
                    .Where(r => r.Game.GameName.Contains(gameName))
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();

                if (!reviews.Any())
                {
                    Console.WriteLine($"Отзывы для игры '{gameName}' не найдены.");
                    return;
                }

                Console.WriteLine($"\nОтзывы по игре: {gameName}");

                foreach (var review in reviews)
                {
                    Console.WriteLine($"Пользователь: {review.User.Nickname} | Оценка: {review.Rating} | Дата: {review.CreatedAt.ToShortDateString()}");
                    Console.WriteLine($"Текст: {review.ReviewText}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
    }
}
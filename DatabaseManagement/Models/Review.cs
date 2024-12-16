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

        public void ViewReviewsAboutThisGame() 
        {
            using(var context = new GameLibraryContext())
            {
                Console.WriteLine("Напиши для какой игры надо посмотреть отзывы: ");
                string gameName = Console.ReadLine();
                var reviews = context.Reviews
                    .Include(r => r.User) 
                    .Include(r => r.Game) 
                    .Where(r => r.Game.GameName.Contains(gameName)) //Фильтрация по названию игры
                    .OrderByDescending(r => r.CreatedAt) //Сортировка по дате
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
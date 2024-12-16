using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using static DatabaseManagement.PasswordHelper;

namespace DatabaseManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }

        public void Information(int currentUserId)
        {
            using(var context = new GameLibraryContext())
            {
                var user = context.Users.Include(u => u.Libraries).Include(u => u.Wishlists)
                    .SingleOrDefault(u => u.UserId == currentUserId);

                //Console.WriteLine("User ID = " + currentUserId);

                if (user == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }

                Console.WriteLine("Ваша информация: ");
                Console.WriteLine($"Логин: {user.Login}");
                Console.WriteLine($"Никнейм: {user.Nickname}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Количество игр в библиотеке: {user.Libraries.Count}");
                Console.WriteLine($"Количество желаемых игр: {user.Wishlists.Count}");
            }
        }
        
        public void AllUserReviews(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var reviews = context.Reviews
                    .Include(r => r.Game) 
                    .Where(r => r.UserId == currentUserId) //Фильтрация по ID пользователя
                    .OrderByDescending(r => r.CreatedAt) //Сортировка по дате добавления
                    .ToList();

                if (!reviews.Any())
                {
                    Console.WriteLine("\nУ вас пока нет отзывов.");
                    return;
                }

                Console.WriteLine("\nВаши отзывы:");
                foreach (var review in reviews)
                {
                    Console.WriteLine($"Игра: {review.Game.GameName}");
                    Console.WriteLine($"Оценка: {review.Rating} | Дата: {review.CreatedAt.ToShortDateString()}");
                    Console.WriteLine($"Текст отзыва: {review.ReviewText}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
        public void DeleteUser()
        {

        }

    }
}
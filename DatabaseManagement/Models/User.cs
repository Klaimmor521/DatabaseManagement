using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

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
            using (var context = new GameLibraryContext())
            {
                var user = context.Users.Include(u => u.Libraries).Include(u => u.Wishlists)
                    .SingleOrDefault(u => u.UserId == currentUserId);

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
                    .Where(r => r.UserId == currentUserId)
                    .OrderByDescending(r => r.CreatedAt)
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

        public void DeleteUser(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        int userIdToDelete = currentUserId;

                        var reviews = context.Reviews.Where(r => r.UserId == userIdToDelete);
                        context.Reviews.RemoveRange(reviews);

                        var libraries = context.Libraries.Where(l => l.UserId == userIdToDelete);
                        context.Libraries.RemoveRange(libraries);

                        var wishlists = context.Wishlists.Where(w => w.UserId == userIdToDelete);
                        context.Wishlists.RemoveRange(wishlists);

                        var user = context.Users.Find(userIdToDelete);
                        if (user != null)
                        {
                            context.Users.Remove(user);
                        }

                        context.SaveChanges();
                        transaction.Commit();

                        Console.WriteLine("Пользователь и все связанные данные успешно удалены.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Ошибка: {ex.Message}. Операция отменена.");
                    }
                }
            }
        }
    }
}

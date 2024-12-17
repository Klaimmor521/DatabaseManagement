using System;
using System.Linq;
using System.Data.Entity;

namespace DatabaseManagement.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual Game Game { get; set; }

        public void WishlistMenu(int currentUserId)
        {
            Console.WriteLine("\nТип:");
            Console.WriteLine("1. Список желаемых игр");
            Console.WriteLine("2. Удалить игру из списка");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListAllGamesFromWishlist(currentUserId);
                    WishlistMenu(currentUserId);
                    break;
                case "2":
                    DeleteFromWishlist(currentUserId);
                    WishlistMenu(currentUserId);
                    break;
                case "3":
                    UserManager userManager = new UserManager();
                    userManager.ShowProfileMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    WishlistMenu(currentUserId);
                    break;
            }
        }

        public void AddToWishlist(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, которую хотите добавить в список желаемого: ");
                string gameName = Console.ReadLine();

                if (!context.Users.Any(u => u.UserId == currentUserId))
                {
                    Console.WriteLine("Ошибка: Пользователь не существует.");
                    return;
                }

                var game = context.Games.FirstOrDefault(g => g.GameName == gameName);

                if (game == null)
                {
                    Console.WriteLine("Ошибка: Игра с таким названием не найдена.");
                    return;
                }

                var exists = context.Wishlists.Any(w => w.UserId == currentUserId && w.GameId == game.GameId);

                if (exists)
                {
                    Console.WriteLine("Эта игра уже есть в вашем списке желаемого.");
                    return;
                }

                var newWishlistItem = new Wishlist
                {
                    UserId = currentUserId,
                    GameId = game.GameId,
                    AddedDate = DateTime.Now
                };

                context.Wishlists.Add(newWishlistItem);
                context.SaveChanges();

                Console.WriteLine($"Игра '{game.GameName}' успешно добавлена в ваш список желаемого.");
            }
        }

        public void DeleteFromWishlist(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, которую хотите удалить из списка желаемого: ");
                string gameName = Console.ReadLine();

                var wishlistItem = context.Wishlists
                    .Include(w => w.Game)
                    .FirstOrDefault(w => w.UserId == currentUserId && w.Game.GameName == gameName);

                if (wishlistItem == null)
                {
                    Console.WriteLine("Ошибка: Игра не найдена в вашем списке желаемого.");
                    return;
                }

                context.Wishlists.Remove(wishlistItem);
                context.SaveChanges();

                Console.WriteLine($"Игра '{wishlistItem.Game.GameName}' успешно удалена из вашего списка желаемого.");
            }
        }

        public void ListAllGamesFromWishlist(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var wishlist = context.Wishlists
                    .Include(w => w.Game)
                    .Where(w => w.UserId == currentUserId)
                    .OrderByDescending(w => w.AddedDate)
                    .ToList();

                if (!wishlist.Any())
                {
                    Console.WriteLine("\nВаш список желаемых игр пуст.");
                    return;
                }

                Console.WriteLine("\nАктуальные игры из вашего списка желаемых:");
                foreach (var item in wishlist)
                {
                    Console.WriteLine($"Игра: {item.Game.GameName}");
                    Console.WriteLine($"Цена: {item.Game.Price} | Дата выхода: {item.Game.ReleaseDate.ToShortDateString()}");
                    Console.WriteLine($"Добавлена в список: {item.AddedDate.ToShortDateString()}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }
    }
}

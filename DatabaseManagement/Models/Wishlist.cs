using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            UserManager userManager = new UserManager();
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
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }

        public void AddToWishlist(int currentUserId)
        {

        }
        public void DeleteFromWishlist(int currentUserId)
        {

        }
        public void ListAllGamesFromWishlist(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var wishlist = context.Wishlists
                    .Include(w => w.Game) 
                    .Where(w => w.UserId == currentUserId) //Фильтрация по ID пользователя
                    .OrderByDescending(w => w.AddedDate) //Сортировка по дате добавления
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
using System;
using System.Collections.Generic;

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
        public void AddGameInLibrary() { }
        public void ViewGames() 
        {
            Review review = new Review();
            UserManager userManager = new UserManager();
            Console.WriteLine("\nТип:");
            Console.WriteLine("1. По дате выпуска");
            Console.WriteLine("3. По жанру");
            Console.WriteLine("4. По цене");
            Console.WriteLine("5. По имени");
            Console.WriteLine("6. По количеству");
            Console.WriteLine("7. Вывести те, которые есть у друзей");
            Console.WriteLine("8. По отзывам");
            Console.WriteLine("9. Назад");
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
                    ViewGames();
                    break;
                case "5":
                    ViewGames();
                    break;
                case "6":
                    ViewGames();
                    break;
                case "7":
                    ViewGames();
                    break;
                case "8":
                    ViewGames();
                    break;
                case "9":
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
            //доделать
        }
    }
}
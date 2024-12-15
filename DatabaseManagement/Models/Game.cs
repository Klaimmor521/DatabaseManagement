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
            Console.WriteLine("2. По жанру");
            Console.WriteLine("3. По цене");
            Console.WriteLine("4. По имени");
            Console.WriteLine("5. По количеству");
            Console.WriteLine("6. Вывести те, которые есть у друзей");
            Console.WriteLine("7. По отзывам");
            Console.WriteLine("8. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ByReleaseDate();
                    break;
                case "2":
                    ByGenre();
                    break;
                case "3":
                    ByPrice();
                    break;
                case "4":
                    ByName();
                    break;
                case "5":
                    ByQuantity();
                    break;
                case "6":
                    OutputThatFriendsHave();
                    break;
                case "7":
                    ByReviews();
                    break;
                case "8":
                    GameMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }

        public void ByReleaseDate()
        {

        }
        public void ByGenre()
        {

        }
        public void ByPrice()
        {

        }
        public void ByName()
        {

        }
        public void ByQuantity()
        {

        }
        public void OutputThatFriendsHave()
        {

        }

        public void OutputReviewsByPositive()
        {

        }
        public void OutputReviewsByNegative()
        {

        }

        public void ByReviews()
        {
            Review review = new Review();
            UserManager userManager = new UserManager();
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
    }
}
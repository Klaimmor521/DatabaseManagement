using System;
using System.Collections.Generic;

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

        public void ReviewMenu() 
        {
            UserManager userManager = new UserManager();
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
                    break;
                case "2":
                    AddReview();
                    break;
                case "3":
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void AddReview() { }
        public void UpdateReview() { }
        public void DeleteReview() { }
        public void ViewReviewsAboutThisGame() { }
    }
}
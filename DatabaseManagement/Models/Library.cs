using System;
using System.Collections.Generic;

namespace DatabaseManagement.Models
{
    public class Library
    {
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public void LibraryMenu() 
        {
            UserManager userManager = new UserManager();
            Console.WriteLine("\nБиблиотека:");
            Console.WriteLine("1. Посмотреть список игр");
            Console.WriteLine("2. Удалить игру из библиотеки");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewGamesInLibrary();
                    break;
                case "2":
                    DeleteGameInLibrary();
                    break;
                case "3":
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void DeleteGameInLibrary() 
        { 

        }
        public void ViewGamesInLibrary() 
        {
            Console.WriteLine("\nТип:");
            Console.WriteLine("1. По убыванию");
            Console.WriteLine("2. По возрастанию");
            Console.WriteLine("3. По жанру");
            Console.WriteLine("4. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Descending();
                    break;
                case "2":
                    Ascending();
                    break;
                case "3":
                    WithGenres();
                    break;
                case "4":
                    LibraryMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void Descending()
        {

        }
        public void Ascending()
        {

        }
        public void WithGenres()
        {

        }
    }
}
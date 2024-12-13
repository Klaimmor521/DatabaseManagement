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
                    return;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void DeleteGameInLibrary() { }
        public void ViewGamesInLibrary() { }
    }
}
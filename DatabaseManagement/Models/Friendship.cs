using System;
using System.Collections.Generic;

namespace DatabaseManagement.Models
{
    public class Friendship
    {
        public int FriendId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

        public void FriendshipMenu() 
        {
            User user = new User();
            UserManager userManager = new UserManager();
            Console.WriteLine("\nДрузья:");
            Console.WriteLine("1. Список пользователей");
            Console.WriteLine("2. Добавить в друзья");
            Console.WriteLine("3. Список всех друзей");
            Console.WriteLine("4. Посмотреть список игр у друга");
            Console.WriteLine("5. Убрать друга из друзей");
            Console.WriteLine("6. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    user.ViewUsers();
                    break;
                case "2":
                    AddNewFriend();
                    break;
                case "3":
                    ViewFriends();
                    break;
                case "4":
                    ViewListFriendGames();
                    break;
                case "5":
                    DeleteFriendship();
                    break;
                case "6":
                    userManager.ShowProfileMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void AddNewFriend() { }
        public void DeleteFriendship() { }
        public void ViewFriends() { }
        public void ViewListFriendGames() { }
    }
}
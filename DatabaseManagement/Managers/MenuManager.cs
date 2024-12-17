using System;

namespace DatabaseManagement
{
    public class MenuManager
    {
       private UserManager _userManager = new UserManager();
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("Добро пожаловать в игровую библиотеку! Выберите действие:");
                Console.WriteLine("1. Регистрация");
                Console.WriteLine("2. Логин");
                Console.WriteLine("3. Выход");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _userManager.Register();
                        break;
                    case "2":
                        _userManager.Login();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }
    }
}
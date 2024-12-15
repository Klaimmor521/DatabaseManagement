using System;
using System.Linq;
using DatabaseManagement.Models;
using static DatabaseManagement.PasswordHelper;

namespace DatabaseManagement
{
    public class UserManager
    {
        private int _currentUserId;
        Library library = new Library();
        Game game = new Game();
        Friendship friendship = new Friendship();
        Review review = new Review();
        User user = new User();
        public void Register()
        {
            Console.WriteLine("Регистрация:");
            string login = GetValidatedInput("Введите логин (минимум 3 символа): ", input => input.Length >= 3, "Логин должен содержать минимум 3 символа.");
            string nickname = GetValidatedInput("Введите никнейм: ", input => input.Length >= 3, "Логин должен содержать минимум 3 символа.");
            string email = GetValidatedInput("Введите email: ", input => input.Contains("@"), "Email должен содержать '@'.");
            string password = GetValidatedInput("Введите пароль (минимум 8 символов, 1 заглавная буква, 1 цифра): ", ValidatePassword, "Пароль должен быть минимум 8 символов, содержать хотя бы одну заглавную букву и одну цифру.");

            string hashedPassword = HashPassword(password);

            using (var context = new GameLibraryContext())
            {
                //Проверка уникальности логина и email
                if (context.Users.Any(u => u.Login == login))
                {
                    Console.WriteLine("Логин уже занят.");
                    return;
                }
                if (context.Users.Any(u => u.Email == email))
                {
                    Console.WriteLine("Email уже занят.");
                    return;
                }
                //Сохранение пользователя
                var user = new User
                {
                    Nickname = nickname,
                    Login = login,
                    Email = email,
                    Password = hashedPassword
                };
                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Регистрация успешна!");
            }
        }
        public void Login()
        {
            Console.WriteLine("Вход:");
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            using (var context = new GameLibraryContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Login == login);
                if (user == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }
                //Проверка пароля
                if (!VerifyPassword(password, user.Password))
                {
                    Console.WriteLine("Неверный пароль.");
                    return;
                }
                _currentUserId = user.UserId; //Сохранение ID текущего пользователя
                Console.WriteLine("Вход выполнен успешно!");
                ShowProfileMenu(); //Переход в меню Профиля
            }
        }
        public void ShowProfileMenu()
        {
            Console.WriteLine("Это Ваша игровая библиотека!");
            Console.WriteLine("Вы в Меню профиля! Выберите действие:");
            Console.WriteLine("1. Библиотека");
            Console.WriteLine("2. Игры");
            Console.WriteLine("3. Профиль");
            Console.WriteLine("4. Назад");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    library.LibraryMenu();
                    break;
                case "2":
                    game.GameMenu();
                    break;
                case "3":
                    UserMenu();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        private void UserMenu()
        {
            Console.WriteLine("\nПрофиль:");
            Console.WriteLine("1. Информация");
            Console.WriteLine("2. Обновить имя");
            Console.WriteLine("3. Обновить email");
            Console.WriteLine("4. Обновить пароль");
            Console.WriteLine("5. Друзья");
            Console.WriteLine("6. Мои отзывы");
            Console.WriteLine("7. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    user.Information();
                    break;
                case "2":
                    user.UpdateName();
                    break;
                case "3":
                    user.UpdateEmail();
                    break;
                case "4":
                    user.UpdatePassword();
                    break;
                case "5":
                    friendship.FriendshipMenu();
                    break;
                case "6":
                    UserReviews();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        private void UserReviews()
        {
            Console.WriteLine("\nМои отзывы:");
            Console.WriteLine("1. Список моих отзывов");
            Console.WriteLine("2. Удалить отзыв");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    user.AllUserReviews();
                    break;
                case "2":
                    review.DeleteReview();
                    break;
                case "3":
                    UserMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        private string GetValidatedInput(string prompt, Func<string, bool> validate, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (validate(input))
                    return input;

                Console.WriteLine(errorMessage);
            }
        }
    }
}
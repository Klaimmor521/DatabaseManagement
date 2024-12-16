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
        Review review = new Review();
        User user = new User();
        Wishlist wishlist = new Wishlist();
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
                //Console.WriteLine("User ID = " + _currentUserId);
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
                    library.LibraryMenu(_currentUserId);
                    break;
                case "2":
                    game.GameMenu();
                    break;
                case "3":
                    UserMenu(_currentUserId);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        private void UserMenu(int currentUserId)
        {
            Console.WriteLine("\nПрофиль:");
            Console.WriteLine("1. Информация");
            Console.WriteLine("2. Обновить имя");
            Console.WriteLine("3. Обновить email");
            Console.WriteLine("4. Обновить пароль");
            Console.WriteLine("5. Список желаемых игр");
            Console.WriteLine("6. Мои отзывы");
            Console.WriteLine("7. Удалить аккаунт");
            Console.WriteLine("8. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    user.Information(_currentUserId);
                    ShowProfileMenu();
                    break;
                case "2":
                    UpdateName(_currentUserId);
                    ShowProfileMenu();
                    break;
                case "3":
                    UpdateEmail(_currentUserId);
                    ShowProfileMenu();
                    break;
                case "4":
                    UpdatePassword(_currentUserId);
                    ShowProfileMenu();
                    break;
                case "5":
                    wishlist.WishlistMenu(_currentUserId);
                    ShowProfileMenu();
                    break;
                case "6":
                    UserReviews();
                    ShowProfileMenu();
                    break;
                case "7":
                    user.DeleteUser();
                    break;
                case "8":
                    game.GameMenu();
                    break;
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
            Console.WriteLine("3. Редактировать отзыв");
            Console.WriteLine("4. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    user.AllUserReviews(_currentUserId);
                    break;
                case "2":
                    DeleteReview(_currentUserId);
                    break;
                case "3":
                    UpdateReview(_currentUserId);
                    break;
                case "4":
                    UserMenu(_currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void UpdateName(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                if (user == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }

                Console.Write("Введите новой никнейм: ");
                string newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine("Имя не может быть пустым.");
                    return;
                }

                user.Nickname = newName;

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Имя пользователя успешно обновлено.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении имени: {ex.Message}");
                }
            }
        }
        public void ReviewMenu()
        {
            Game game = new Game();
            Console.WriteLine("\nОтзывы:");
            Console.WriteLine("1. Посмотреть отзывы об игре");
            Console.WriteLine("2. Добавить отзыв");
            Console.WriteLine("3. Назад");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    review.ViewReviewsAboutThisGame();
                    game.GameMenu();
                    break;
                case "2":
                    AddReview(_currentUserId);
                    game.GameMenu();
                    break;
                case "3":
                    game.GameMenu();
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    break;
            }
        }
        public void AddReview(int currentUserId)
        {

        }
        public void UpdateReview(int currentUserId)
        {

        }
        public void DeleteReview(int currentUserId)
        {

        }
        public void UpdateEmail(int currentUserId)
        {

        }
        public void UpdatePassword(int currentUserId)
        {

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
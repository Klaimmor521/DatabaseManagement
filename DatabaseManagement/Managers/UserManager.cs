using System;
using System.Linq;
using System.Data.Entity;
using DatabaseManagement.Models;
using static DatabaseManagement.PasswordHelper;
using Spectre.Console;

namespace DatabaseManagement
{
    public class UserManager
    {
        private int _currentUserId;

        Library library = new Library();
        Game game = new Game();
        User user = new User();
        Wishlist wishlist = new Wishlist();

        public void Register()
        {
            Console.WriteLine("Регистрация:");
            string login = GetValidatedInput("Введите логин (минимум 3 символа): ", input => input.Length >= 3, "Логин должен содержать минимум 3 символа.");
            string nickname = GetValidatedInput("Введите никнейм: ", input => input.Length >= 3, "Никнейм должен содержать минимум 3 символа.");
            string email = GetValidatedInput("Введите email: ", input => input.Contains("@"), "Email должен содержать '@'.");
            string password = GetValidatedInput("Введите пароль (минимум 8 символов, 1 заглавная буква, 1 цифра): ", ValidatePassword, "Пароль должен быть минимум 8 символов, содержать хотя бы одну заглавную букву и одну цифру.");

            string hashedPassword = HashPassword(password);

            using (var context = new GameLibraryContext())
            {
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

                var newUser = new User
                {
                    Nickname = nickname,
                    Login = login,
                    Email = email,
                    Password = hashedPassword
                };
                context.Users.Add(newUser);
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

                if (!VerifyPassword(password, user.Password))
                {
                    Console.WriteLine("Неверный пароль.");
                    return;
                }

                _currentUserId = user.UserId;
                Console.WriteLine("Вход выполнен успешно!");
                ShowProfileMenu(_currentUserId);
            }
        }

        public void ShowProfileMenu(int currentUserId)
        {
            //Информационный текст
            AnsiConsole.MarkupLine("[bold cyan]Это Ваша игровая библиотека![/]");
            AnsiConsole.MarkupLine("[bold yellow]Вы в Меню профиля! Выберите действие:[/]");

            //Меню выбора действий
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold white]Выберите действие:[/]")
                    .AddChoices(new[]
                    {
                "1. Библиотека",
                "2. Игры",
                "3. Профиль",
                "4. Назад"
                    })
                    .HighlightStyle("cyan"));

            //Обработка выбора
            switch (choice)
            {
                case "1. Библиотека":
                    library.LibraryMenu(currentUserId);
                    break;
                case "2. Игры":
                    game.GameMenu(currentUserId);
                    break;
                case "3. Профиль":
                    UserMenu(currentUserId);
                    break;
                case "4. Назад":
                    MenuManager menuManager = new MenuManager();
                    menuManager.ShowMainMenu();
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Неверный выбор, попробуйте снова.[/]");
                    ShowProfileMenu(currentUserId);
                    break;
            }
        }

        private void UserMenu(int currentUserId)
        {
            // Заголовок с рамкой и цветом
            AnsiConsole.Write(
                new FigletText("Profile")
                    .Color(Color.Cyan1));

            // Меню выбора действий
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Выберите действие в профиле:[/]")
                    .AddChoices(new[]
                    {
                "1. Информация",
                "2. Обновить имя",
                "3. Обновить email",
                "4. Обновить пароль",
                "5. Список желаемых игр",
                "6. Мои отзывы",
                "7. Удалить аккаунт",
                "8. Назад"
                    })
                    .HighlightStyle("yellow"));

            // Обработка выбора пользователя
            switch (choice)
            {
                case "1. Информация":
                    user.Information(currentUserId);
                    UserMenu(currentUserId);
                    break;

                case "2. Обновить имя":
                    UpdateName(currentUserId);
                    UserMenu(currentUserId);
                    break;

                case "3. Обновить email":
                    UpdateEmail(currentUserId);
                    UserMenu(currentUserId);
                    break;

                case "4. Обновить пароль":
                    UpdatePassword(currentUserId);
                    UserMenu(currentUserId);
                    break;

                case "5. Список желаемых игр":
                    wishlist.WishlistMenu(currentUserId);
                    UserMenu(currentUserId);
                    break;

                case "6. Мои отзывы":
                    UserReviews(currentUserId);
                    UserMenu(currentUserId);
                    break;

                case "7. Удалить аккаунт":
                    user.DeleteUser(currentUserId);
                    break;

                case "8. Назад":
                    ShowProfileMenu(currentUserId);
                    break;

                default:
                    AnsiConsole.MarkupLine("[bold red]Неверный выбор, попробуйте снова.[/]");
                    UserMenu(currentUserId);
                    break;
            }
        }

        private void UserReviews(int currentUserId)
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
                    user.AllUserReviews(currentUserId);
                    break;
                case "2":
                    DeleteReview(currentUserId);
                    break;
                case "3":
                    UpdateReview(currentUserId);
                    break;
                case "4":
                    UserMenu(currentUserId);
                    break;
                default:
                    Console.WriteLine("Неверный выбор, попробуйте снова.");
                    UserReviews(currentUserId);
                    break;
            }
        }

        public void UpdateName(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                if (userToUpdate == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }

                string newName = GetValidatedInput("Введите никнейм: ", input => input.Length >= 3, "Никнейм должен содержать минимум 3 символа.");

                userToUpdate.Nickname = newName;

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

        public void UpdateReview(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, отзыв для которой хотите обновить: ");
                string gameName = Console.ReadLine();

                var review = context.Reviews
                    .Include(r => r.Game)
                     .FirstOrDefault(r => r.UserId == currentUserId && r.Game.GameName.ToLower() == gameName.ToLower());

                if (review == null)
                {
                    Console.WriteLine("Ошибка: Отзыв для указанной игры не найден.");
                    return;
                }

                int rating;
                while (true)
                {
                    Console.Write("Введите оценку (1-10): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out rating) && rating >= 1 && rating <= 10)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Оценка должна быть числом от 1 до 10. Попробуйте снова.");
                    }
                }

                string reviewText;
                while (true)
                {
                    Console.Write("Введите текст отзыва: ");
                    reviewText = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(reviewText))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: Текст отзыва не может быть пустым. Попробуйте снова.");
                    }
                }

                review.Rating = rating;
                review.ReviewText = reviewText;
                review.CreatedAt = DateTime.Now;

                context.SaveChanges();

                Console.WriteLine("Отзыв успешно обновлён!");
            }
        }

        public void DeleteReview(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                Console.Write("Введите название игры, отзыв для которой хотите удалить: ");
                string gameName = Console.ReadLine();

                var review = context.Reviews
                    .Include(r => r.Game)
                    .FirstOrDefault(r => r.UserId == currentUserId && r.Game.GameName == gameName);

                if (review == null)
                {
                    Console.WriteLine("Ошибка: Отзыв для данной игры не найден.");
                    return;
                }

                context.Reviews.Remove(review);
                context.SaveChanges();

                Console.WriteLine($"Отзыв для игры '{review.Game.GameName}' успешно удалён.");
            }
        }

        public void UpdateEmail(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                if (userToUpdate == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }

                string newEmail = GetValidatedInput("Введите email: ", input => input.Contains("@"), "Email должен содержать '@'.");

                userToUpdate.Email = newEmail;

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Email пользователя успешно обновлен.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении email: {ex.Message}");
                }
            }
        }

        public void UpdatePassword(int currentUserId)
        {
            using (var context = new GameLibraryContext())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == currentUserId);

                if (userToUpdate == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }

                string newPassword = GetValidatedInput("Введите пароль (минимум 8 символов, 1 заглавная буква, 1 цифра): ", ValidatePassword, "Пароль должен быть минимум 8 символов, содержать хотя бы одну заглавную букву и одну цифру.");
                string hashedPassword = HashPassword(newPassword);
                userToUpdate.Password = hashedPassword;

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Пароль пользователя успешно обновлен.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении пароля: {ex.Message}");
                }
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
using Spectre.Console;
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
                Console.Clear();
                //Вывод заголовка с рамкой
                AnsiConsole.Write(
                    new FigletText("Game Library")
                        .Color(Color.Green));

                //Используем красивое дерево меню
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Добро пожаловать в игровую библиотеку![/] [grey](Используйте стрелки для выбора)[/]")
                        .PageSize(5)
                        .AddChoices(new[]
                        {
                            "🔹 Регистрация",
                            "🔑 Логин",
                            "🚪 Выход"
                        }));

                //Обработка выбора
                switch (choice)
                {
                    case "🔹 Регистрация":
                        _userManager.Register();
                        break;
                    case "🔑 Логин":
                        _userManager.Login();
                        break;
                    case "🚪 Выход":
                        AnsiConsole.MarkupLine("[red]До свидания![/]");
                        return;
                    default:
                        AnsiConsole.MarkupLine("[red]Неверный выбор, попробуйте снова.[/]");
                        break;
                }
            }
        }
    }
}
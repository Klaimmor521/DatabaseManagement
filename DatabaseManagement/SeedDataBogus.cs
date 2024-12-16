using Bogus;
using DatabaseManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using static DatabaseManagement.PasswordHelper;

namespace DatabaseManagement
{
    public class SeedDataBogus
    {
        public static void Seed()
        {
            using (var context = new GameLibraryContext())
            {
                //Генерация пользователей
                //var userFaker = new Faker<User>()
                //.RuleFor(u => u.Nickname, f => f.Internet.UserName())
                //.RuleFor(u => u.Login, (f, u) => f.Internet.UserName().Replace(".", "") + f.Random.Number(1, 10000)) //Уникальный Login
                //.RuleFor(u => u.Email, (f, u) => $"{u.Login}@example.com") //Генерация Email на основе уникального Login
                //.RuleFor(u => u.Password, f => HashPassword(f.Internet.Password()));
                ////Генерация пользователей
                //var users = userFaker.Generate(88);
                ////Фильтрация дубликатов перед вставкой
                //var uniqueUsers = users.Where(user =>
                //    !context.Users.Any(u => u.Login == user.Login || u.Email == user.Email)).ToList();
                ////Добавление уникальных пользователей
                //if (uniqueUsers.Any())
                //{
                //    context.Users.AddRange(uniqueUsers);
                //    context.SaveChanges();
                //}

                //Генерация игр
                //var gameFaker = new Faker<Game>()
                //    .RuleFor(g => g.GameName, f => f.Commerce.ProductName())
                //    .RuleFor(g => g.Price, f => f.Random.Decimal(10, 100))
                //    .RuleFor(g => g.ReleaseDate, f => f.Date.Past(5))
                //    .RuleFor(g => g.GenreId, f => f.Random.Number(1, 50))
                //    .RuleFor(g => g.GamePlatformId, f => f.Random.Number(1, 50));

                //var games = gameFaker.Generate(100);
                //context.Games.AddRange(games);
                //context.SaveChanges();

                //Генерация отзывов
                //var existingUserIds = context.Users.Select(u => u.UserId).ToList();
                //var existingGameIds = context.Games.Select(g => g.GameId).ToList();

                //var reviewFaker = new Faker<Review>()
                //    .RuleFor(r => r.ReviewText, f => f.Rant.Review())
                //    .RuleFor(r => r.Rating, f => f.Random.Number(1, 10))
                //    .RuleFor(r => r.CreatedAt, f => f.Date.Recent(30))
                //    .RuleFor(r => r.UserId, f => f.PickRandom(existingUserIds))
                //    .RuleFor(r => r.GameId, f => f.PickRandom(existingGameIds));

                //var reviews = reviewFaker.Generate(8);
                //context.Reviews.AddRange(reviews);
                //context.SaveChanges();
                Console.WriteLine("Данные успешно добавлены!");
            }
        }
    }
}
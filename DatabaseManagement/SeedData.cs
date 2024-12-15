using Bogus;
using DatabaseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DatabaseManagement.PasswordHelper;

namespace DatabaseManagement
{
    public class SeedData
    {
        public static void Seed()
        {
            using (var context = new GameLibraryContext())
            {
                ////Генерация жанров
                //if (!context.Genres.Any())
                //{
                //    var genreFaker = new Faker<Genre>()
                //        .RuleFor(g => g.GenreName, f => f.Commerce.Department());

                //    var genres = genreFaker.Generate(2);
                //    context.Genres.AddRange(genres);
                //    context.SaveChanges();
                //}

                ////Генерация платформ
                //if (!context.GamePlatforms.Any())
                //{
                //    var platformFaker = new Faker<GamePlatform>()
                //        .RuleFor(p => p.PlatformName, f => f.Commerce.ProductName());

                //    var platforms = platformFaker.Generate(20);
                //    context.GamePlatforms.AddRange(platforms);
                //    context.SaveChanges();
                //}

                ////Генерация пользователей
                //if (!context.Users.Any())
                //{
                //    var userFaker = new Faker<User>()
                //        .RuleFor(u => u.Nickname, f => f.Internet.UserName())
                //        .RuleFor(u => u.Login, f => f.Internet.UserName())
                //        .RuleFor(u => u.Email, f => f.Internet.Email())
                //        .RuleFor(u => u.Password, f => HashPassword(f.Internet.Password()));

                //    var users = userFaker.Generate(500);
                //    context.Users.AddRange(users);
                //    context.SaveChanges();
                //}

                ////Генерация игр
                //if (!context.Games.Any())
                //{
                //    var gameFaker = new Faker<Game>()
                //        .RuleFor(g => g.GameName, f => f.Commerce.ProductName())
                //        .RuleFor(g => g.Price, f => f.Random.Decimal(10, 100))
                //        .RuleFor(g => g.ReleaseDate, f => f.Date.Past(5))
                //        .RuleFor(g => g.GenreId, f => f.Random.Number(1, 50))
                //        .RuleFor(g => g.GamePlatformId, f => f.Random.Number(1, 50));

                //    var games = gameFaker.Generate(400);
                //    context.Games.AddRange(games);
                //    context.SaveChanges();
                //}

                ////Генерация отзывов
                //if (!context.Reviews.Any())
                //{
                //    var reviewFaker = new Faker<Review>()
                //        .RuleFor(r => r.ReviewText, f => f.Rant.Review())
                //        .RuleFor(r => r.Rating, f => f.Random.Number(1, 10))
                //        .RuleFor(r => r.CreatedAt, f => f.Date.Recent(30))
                //        .RuleFor(r => r.UserId, f => f.Random.Number(1, 500))
                //        .RuleFor(r => r.GameId, f => f.Random.Number(1, 400));

                //    var reviews = reviewFaker.Generate(1000);
                //    context.Reviews.AddRange(reviews);
                //    context.SaveChanges();
                //}

                Console.WriteLine("Данные успешно добавлены!");
            }
        }
    }
}
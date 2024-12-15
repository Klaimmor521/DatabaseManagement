using DatabaseManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement
{
    public class UsualSeedData
    {
        public static async Task SeedAsync()
        {
            using (var context = new GameLibraryContext())
            {
                //await InsertGenres(context, 48);        //Жанры
                //await InsertGamePlatforms(context, 50); //Платформы
                //await InsertUsers(context, 500);        //Пользователи
                //await InsertGames(context, 900);        //Игры
                //await InsertReviews(context, 500);      //Отзывы
                //await InsertFriendships(context, 50);
            }
        }

        private static async Task InsertGenres(GameLibraryContext context, int count)
        {
            //if (!context.Genres.Any())
            //{
                var genres = Enumerable.Range(1, count)
                    .Select(i => new Genre { GenreName = $"Genre {i}" })
                    .ToList();

                context.Genres.AddRange(genres);
                await context.SaveChangesAsync();
            //}
        }

        private static async Task InsertGamePlatforms(GameLibraryContext context, int count)
        {
            if (!context.GamePlatforms.Any())
            {
                var platforms = Enumerable.Range(1, count)
                    .Select(i => new GamePlatform { PlatformName = $"Platform {i}" })
                    .ToList();

                context.GamePlatforms.AddRange(platforms);
                await context.SaveChangesAsync();
            }
        }

        //private static async Task InsertFriendships(GameLibraryContext context, int count)
        //{
        //    if (!context.Friendships.Any())
        //    {
        //        var userIds = context.Users.Select(u => u.UserId).ToList();
        //        var friendships = new HashSet<(int, int)>();

        //        var random = new Random();

        //        while (friendships.Count < count)
        //        {
        //            int userId1 = userIds[random.Next(userIds.Count)];
        //            int userId2 = userIds[random.Next(userIds.Count)];

        //            //Уникальность пар: сам себе не друг и нет дублирования
        //            if (userId1 != userId2 && friendships.Add((Math.Min(userId1, userId2), Math.Max(userId1, userId2))))
        //            {
        //                //Добавляем только уникальные пары
        //                context.Friendships.Add(new Friendship
        //                {
        //                    UserId1 = userId1,
        //                    UserId2 = userId2
        //                });
        //            }
        //        }

        //        await context.SaveChangesAsync();
        //    }
        //}

        //private static async Task InsertUsers(GameLibraryContext context, int count)
        //{
        //    if (!context.Users.Any())
        //    {
        //        var users = Enumerable.Range(1, count)
        //            .Select(i => new User
        //            {
        //                Nickname = $"User{i}",
        //                Login = $"login{i}",
        //                Email = $"user{i}@example.com",
        //                Password = $"password_hash_{i}"
        //            })
        //            .ToList();

        //        context.Users.AddRange(users);
        //        await context.SaveChangesAsync();
        //    }
        //}

        private static async Task InsertGames(GameLibraryContext context, int count)
        {
            if (!context.Games.Any())
            {
                var random = new Random();
                var games = Enumerable.Range(1, count)
                    .Select(i => new Game
                    {
                        GameName = $"Game {i}",
                        Price = random.Next(100, 10000) / 100.0m,
                        GenreId = random.Next(1, 51),        
                        GamePlatformId = random.Next(1, 51), 
                        ReleaseDate = DateTime.Now.AddDays(-random.Next(1, 365))
                    })
                    .ToList();

                context.Games.AddRange(games);
                await context.SaveChangesAsync();
            }
        }

        private static async Task InsertReviews(GameLibraryContext context, int count)
        {
            if (!context.Reviews.Any())
            {
                var random = new Random();
                var reviews = Enumerable.Range(1, count)
                    .Select(i => new Review
                    {
                        UserId = random.Next(1, 501),  
                        GameId = random.Next(1, 901),  
                        ReviewText = $"Review {i}",
                        Rating = random.Next(1, 11),
                        CreatedAt = DateTime.Now.AddDays(-random.Next(1, 30))
                    })
                    .ToList();

                context.Reviews.AddRange(reviews);
                await context.SaveChangesAsync();
            }
        }
    }
}
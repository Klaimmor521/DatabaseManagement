using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DatabaseManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public void Information(int currentUserId)
        {
            using(var context = new GameLibraryContext())
            {
                var user = context.Users.Include(u => u.Libraries)
                    .SingleOrDefault(u => u.UserId == currentUserId);

                //Console.WriteLine("User ID = " + currentUserId);

                if (user == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return;
                }

                Console.WriteLine("Информация о пользователе:");
                Console.WriteLine($"Логин: {user.Login}");
                Console.WriteLine($"Никнейм: {user.Nickname}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Количество игр в библиотеке: {user.Libraries.Count}");
            }
        }
        public void UpdateName()
        {

        }
        public void UpdateEmail()
        {

        }
        public void UpdatePassword()
        {

        }
        public void AllUserReviews()
        {
            
        }
        public void DeleteUser()
        {

        }
    }
}
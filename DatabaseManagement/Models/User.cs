using System;
using System.Collections.Generic;

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
        public ICollection<Friendship> Friendships { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public void AddUser() { }
        public void UpdateUser() { }
        public void DeleteUser() { }
        public void ViewUsers() { }

        public void Information()
        {

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
    }
}
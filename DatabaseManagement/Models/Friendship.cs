using System;
using System.Collections.Generic;

namespace DatabaseManagement.Models
{
    public class Friendship
    {
        public int FriendId { get; set; }
        public int UserId1 { get; set; }
        public User User1 { get; set; }
        public int UserId2 { get; set; }
        public User User2 { get; set; }

        public void FriendshipMenu() { }
        public void AddNewFriend() { }
        public void DeleteFriendship() { }
        public void ViewFriends() { }
    }
}
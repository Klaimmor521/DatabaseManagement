using System;
using System.Collections.Generic;

namespace DatabaseManagement.Models
{
    public class Library
    {
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }

        public void LibraryMenu() { }
        public void DeleteGameInLibrary() { }
        public void ViewGamesInLibrary() { }
    }
}
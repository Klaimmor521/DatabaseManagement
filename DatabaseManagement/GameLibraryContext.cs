using DatabaseManagement.Models;
using System;
using System.Data.Entity;

namespace DatabaseManagement
{
    public class GameLibraryContext : DbContext
    {
        public GameLibraryContext() : base("master") { }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
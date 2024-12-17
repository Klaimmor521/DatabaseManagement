using DatabaseManagement.Models;
using System;
using System.Data.Entity;

namespace DatabaseManagement
{
    public class GameLibraryContext : DbContext
    {
        public GameLibraryContext() : base("connectionToDatabase") { }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Отключаем автоматическую плюрализацию имён
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            //Настройка таблицы Users
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
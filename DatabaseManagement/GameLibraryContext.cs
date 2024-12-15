using DatabaseManagement.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Отключаем автоматическую плюрализацию имён
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            //Настройка таблицы Users
            modelBuilder.Entity<User>().ToTable("Users");

            //Проблема с User_UserId
            //Настройка таблицы Friendship
            modelBuilder.Entity<Friendship>()
                .HasKey(f => f.FriendId); //Первичный ключ

            modelBuilder.Entity<Friendship>()
                .HasRequired(f => f.InitiatorUser) // Связь с InitiatorUser
                .WithMany()
                .HasForeignKey(f => f.InitiatorUserId)
                .WillCascadeOnDelete(false); // Отключаем каскадное удаление

            modelBuilder.Entity<Friendship>()
                .HasRequired(f => f.RecipientUser) // Связь с RecipientUser
                .WithMany()
                .HasForeignKey(f => f.RecipientUserId)
                .WillCascadeOnDelete(false); // Отключаем каскадное удаление

            base.OnModelCreating(modelBuilder);
        }
    }
}
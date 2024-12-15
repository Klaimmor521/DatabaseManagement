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
            //Удаляет плюрализацию имен таблиц
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friendship>()
                .HasKey(f => f.FriendId); //FriendId — первичный ключ

            modelBuilder.Entity<Friendship>()
                .HasRequired(f => f.User1)
                .WithMany()
                .HasForeignKey(f => f.UserId1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friendship>()
                .HasRequired(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.UserId2)
                .WillCascadeOnDelete(false);
        }
    }
}
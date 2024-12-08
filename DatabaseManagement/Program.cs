using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using DatabaseManagement.Models;

namespace DatabaseManagement
{
    class Program
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionToDatabase"].ConnectionString;
        private static readonly string masterConnectionString = ConfigurationManager.ConnectionStrings["connectionToMaster"].ConnectionString;
        static void Main()
        {
            
        }
        static void CreateDatabaseAndTables()
        {
            string createDatabase = @"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GameLibrary')
            BEGIN
                CREATE DATABASE GameLibrary;
            END";

            string createTables = @"
            USE GameLibrary;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Genre')
            BEGIN
            CREATE TABLE Genre (
                GenreId INT PRIMARY KEY IDENTITY(1,1), 
                GenreName NVARCHAR(50) NOT NULL UNIQUE 
            );
            END;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GamePlatform')
            BEGIN
            CREATE TABLE GamePlatform (
                GamePlatformId INT PRIMARY KEY IDENTITY(1,1), 
                PlatformName NVARCHAR(50) NOT NULL UNIQUE
            );
            END;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
            BEGIN
            CREATE TABLE Users (
                UserId INT PRIMARY KEY IDENTITY(1,1),
                Nickname NVARCHAR(50) NOT NULL,
                Login NVARCHAR(50) NOT NULL UNIQUE,
                Password NVARCHAR(255) NOT NULL,
                Email NVARCHAR(100) NOT NULL UNIQUE
            );
            END;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Game')
            BEGIN
            CREATE TABLE Game (
                GameId INT PRIMARY KEY IDENTITY(1,1),  
                GenreId INT NOT NULL,                  
                GamePlatformId INT NOT NULL,           
                GameName NVARCHAR(100) NOT NULL,       
                Price DECIMAL(10, 2) NOT NULL,         
                ReleaseDate DATE NOT NULL,             
                CONSTRAINT FK_Game_Genre FOREIGN KEY (GenreId) REFERENCES Genre(GenreId) ON DELETE CASCADE,
                CONSTRAINT FK_Game_Platform FOREIGN KEY (GamePlatformId) REFERENCES GamePlatform(GamePlatformId) ON DELETE CASCADE
            );
            END;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Library')
            BEGIN
            CREATE TABLE Library (
                LibraryId INT PRIMARY KEY IDENTITY(1,1), 
                UserId INT NOT NULL,                     
                GameId INT NOT NULL,                     
                CONSTRAINT FK_Library_User FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
                CONSTRAINT FK_Library_Game FOREIGN KEY (GameId) REFERENCES Game(GameId) ON DELETE CASCADE
            );
            END;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Friendship')
            BEGIN
            CREATE TABLE Friendship (
                FriendId INT PRIMARY KEY IDENTITY(1,1),  
                UserId1 INT NOT NULL,                    
                UserId2 INT NOT NULL,                    
                CONSTRAINT FK_Friendship_User1 FOREIGN KEY (UserId1) REFERENCES Users(UserId) ON DELETE CASCADE,
                CONSTRAINT FK_Friendship_User2 FOREIGN KEY (UserId2) REFERENCES Users(UserId) ON DELETE NO ACTION
            );
            END;

            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Review')
            BEGIN
            CREATE TABLE Review (
                ReviewId INT PRIMARY KEY IDENTITY(1,1), 
                UserId INT NOT NULL,                    
                GameId INT NOT NULL,                    
                ReviewText NVARCHAR(MAX),               
                Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 10), 
                CreatedAt DATETIME DEFAULT GETDATE(),   
                CONSTRAINT FK_Review_User FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
                CONSTRAINT FK_Review_Game FOREIGN KEY (GameId) REFERENCES Game(GameId) ON DELETE CASCADE
            );
            END;";


            using (var connection = new SqlConnection(masterConnectionString))
            {
                Console.WriteLine("Начал подключение...");
                connection.Open();
                Console.WriteLine("Подключение установлено...");

                //Создание базы данных
                using (var command = new SqlCommand(createDatabase, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("База данных создана...");
                }

                //Создание таблиц
                using (var command = new SqlCommand(createTables, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Таблицы созданы...");
                }
                Console.WriteLine("Все операции закончены!");
            }
        }
    }
}
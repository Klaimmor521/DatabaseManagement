namespace DatabaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        UserId1 = c.Int(nullable: false),
                        UserId2 = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.UserId1)
                .ForeignKey("dbo.Users", t => t.UserId2)
                .Index(t => t.UserId1)
                .Index(t => t.UserId2)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Libraries",
                c => new
                    {
                        LibraryId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LibraryId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleaseDate = c.DateTime(nullable: false),
                        GenreId = c.Int(nullable: false),
                        GamePlatformId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.GamePlatforms", t => t.GamePlatformId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.GamePlatformId);
            
            CreateTable(
                "dbo.GamePlatforms",
                c => new
                    {
                        GamePlatformId = c.Int(nullable: false, identity: true),
                        PlatformName = c.String(),
                    })
                .PrimaryKey(t => t.GamePlatformId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ReviewText = c.String(),
                        Rating = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "UserId2", "dbo.Users");
            DropForeignKey("dbo.Friendships", "UserId1", "dbo.Users");
            DropForeignKey("dbo.Libraries", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "GameId", "dbo.Games");
            DropForeignKey("dbo.Libraries", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Games", "GamePlatformId", "dbo.GamePlatforms");
            DropForeignKey("dbo.Friendships", "User_UserId", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "GameId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Games", new[] { "GamePlatformId" });
            DropIndex("dbo.Games", new[] { "GenreId" });
            DropIndex("dbo.Libraries", new[] { "GameId" });
            DropIndex("dbo.Libraries", new[] { "UserId" });
            DropIndex("dbo.Friendships", new[] { "User_UserId" });
            DropIndex("dbo.Friendships", new[] { "UserId2" });
            DropIndex("dbo.Friendships", new[] { "UserId1" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Genres");
            DropTable("dbo.GamePlatforms");
            DropTable("dbo.Games");
            DropTable("dbo.Libraries");
            DropTable("dbo.Users");
            DropTable("dbo.Friendships");
        }
    }
}

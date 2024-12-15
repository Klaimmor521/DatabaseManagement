namespace DatabaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedInitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Friendship",
            //    c => new
            //        {
            //            FriendId = c.Int(nullable: false, identity: true),
            //            UserId1 = c.Int(nullable: false),
            //            UserId2 = c.Int(nullable: false),
            //            User_UserId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.FriendId)
            //    .ForeignKey("dbo.Users", t => t.User_UserId)
            //    .ForeignKey("dbo.Users", t => t.UserId1)
            //    .ForeignKey("dbo.Users", t => t.UserId2)
            //    .Index(t => t.UserId1)
            //    .Index(t => t.UserId2)
            //    .Index(t => t.User_UserId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false, identity: true),
            //            Nickname = c.String(),
            //            Login = c.String(),
            //            Password = c.String(),
            //            Email = c.String(),
            //        })
            //    .PrimaryKey(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Library",
            //    c => new
            //        {
            //            LibraryId = c.Int(nullable: false, identity: true),
            //            UserId = c.Int(nullable: false),
            //            GameId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.LibraryId)
            //    .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.GameId);
            
            //CreateTable(
            //    "dbo.Game",
            //    c => new
            //        {
            //            GameId = c.Int(nullable: false, identity: true),
            //            GameName = c.String(),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            ReleaseDate = c.DateTime(nullable: false),
            //            GenreId = c.Int(nullable: false),
            //            GamePlatformId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.GameId)
            //    .ForeignKey("dbo.GamePlatform", t => t.GamePlatformId, cascadeDelete: true)
            //    .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
            //    .Index(t => t.GenreId)
            //    .Index(t => t.GamePlatformId);
            
            //CreateTable(
            //    "dbo.GamePlatform",
            //    c => new
            //        {
            //            GamePlatformId = c.Int(nullable: false, identity: true),
            //            PlatformName = c.String(),
            //        })
            //    .PrimaryKey(t => t.GamePlatformId);
            
            //CreateTable(
            //    "dbo.Genre",
            //    c => new
            //        {
            //            GenreId = c.Int(nullable: false, identity: true),
            //            GenreName = c.String(),
            //        })
            //    .PrimaryKey(t => t.GenreId);
            
            //CreateTable(
            //    "dbo.Review",
            //    c => new
            //        {
            //            ReviewId = c.Int(nullable: false, identity: true),
            //            ReviewText = c.String(),
            //            Rating = c.Int(nullable: false),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            GameId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ReviewId)
            //    .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendship", "UserId2", "dbo.Users");
            DropForeignKey("dbo.Friendship", "UserId1", "dbo.Users");
            DropForeignKey("dbo.Library", "UserId", "dbo.Users");
            DropForeignKey("dbo.Review", "UserId", "dbo.Users");
            DropForeignKey("dbo.Review", "GameId", "dbo.Game");
            DropForeignKey("dbo.Library", "GameId", "dbo.Game");
            DropForeignKey("dbo.Game", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Game", "GamePlatformId", "dbo.GamePlatform");
            DropForeignKey("dbo.Friendship", "User_UserId", "dbo.Users");
            DropIndex("dbo.Review", new[] { "GameId" });
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.Game", new[] { "GamePlatformId" });
            DropIndex("dbo.Game", new[] { "GenreId" });
            DropIndex("dbo.Library", new[] { "GameId" });
            DropIndex("dbo.Library", new[] { "UserId" });
            DropIndex("dbo.Friendship", new[] { "User_UserId" });
            DropIndex("dbo.Friendship", new[] { "UserId2" });
            DropIndex("dbo.Friendship", new[] { "UserId1" });
            DropTable("dbo.Review");
            DropTable("dbo.Genre");
            DropTable("dbo.GamePlatform");
            DropTable("dbo.Game");
            DropTable("dbo.Library");
            DropTable("dbo.Users");
            DropTable("dbo.Friendship");
        }
    }
}

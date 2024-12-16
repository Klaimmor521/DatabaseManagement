namespace DatabaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GamePlatform",
                c => new
                    {
                        GamePlatformId = c.Int(nullable: false, identity: true),
                        PlatformName = c.String(),
                    })
                .PrimaryKey(t => t.GamePlatformId);
            
            CreateTable(
                "dbo.Game",
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
                .ForeignKey("dbo.GamePlatform", t => t.GamePlatformId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.GamePlatformId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Library",
                c => new
                    {
                        LibraryId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LibraryId)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
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
                "dbo.Review",
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
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Wishlist",
                c => new
                    {
                        WishlistId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WishlistId)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wishlist", "UserId", "dbo.Users");
            DropForeignKey("dbo.Wishlist", "GameId", "dbo.Game");
            DropForeignKey("dbo.Review", "UserId", "dbo.Users");
            DropForeignKey("dbo.Review", "GameId", "dbo.Game");
            DropForeignKey("dbo.Library", "UserId", "dbo.Users");
            DropForeignKey("dbo.Library", "GameId", "dbo.Game");
            DropForeignKey("dbo.Game", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Game", "GamePlatformId", "dbo.GamePlatform");
            DropIndex("dbo.Wishlist", new[] { "GameId" });
            DropIndex("dbo.Wishlist", new[] { "UserId" });
            DropIndex("dbo.Review", new[] { "GameId" });
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.Library", new[] { "GameId" });
            DropIndex("dbo.Library", new[] { "UserId" });
            DropIndex("dbo.Game", new[] { "GamePlatformId" });
            DropIndex("dbo.Game", new[] { "GenreId" });
            DropTable("dbo.Wishlist");
            DropTable("dbo.Review");
            DropTable("dbo.Users");
            DropTable("dbo.Library");
            DropTable("dbo.Genre");
            DropTable("dbo.Game");
            DropTable("dbo.GamePlatform");
        }
    }
}

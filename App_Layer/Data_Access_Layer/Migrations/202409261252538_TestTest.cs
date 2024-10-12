namespace Data_Access_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200, unicode: false),
                        Description = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PlaylistSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaylistId = c.Int(nullable: false),
                        SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId)
                .ForeignKey("dbo.Songs", t => t.SongId)
                .Index(t => t.PlaylistId)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200, unicode: false),
                        Artist = c.String(nullable: false, maxLength: 100, unicode: false),
                        Album = c.String(nullable: false, maxLength: 100, unicode: false),
                        Lyrics = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Duration = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecentlyPlayeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.Int(nullable: false),
                        SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.SongId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SharedPlaylists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaylistId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playlists", t => t.PlaylistId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.PlaylistId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playlists", "UserId", "dbo.Users");
            DropForeignKey("dbo.PlaylistSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.RecentlyPlayeds", "UserId", "dbo.Users");
            DropForeignKey("dbo.SharedPlaylists", "UserId", "dbo.Users");
            DropForeignKey("dbo.SharedPlaylists", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.RecentlyPlayeds", "SongId", "dbo.Songs");
            DropForeignKey("dbo.PlaylistSongs", "PlaylistId", "dbo.Playlists");
            DropIndex("dbo.SharedPlaylists", new[] { "UserId" });
            DropIndex("dbo.SharedPlaylists", new[] { "PlaylistId" });
            DropIndex("dbo.RecentlyPlayeds", new[] { "SongId" });
            DropIndex("dbo.RecentlyPlayeds", new[] { "UserId" });
            DropIndex("dbo.PlaylistSongs", new[] { "SongId" });
            DropIndex("dbo.PlaylistSongs", new[] { "PlaylistId" });
            DropIndex("dbo.Playlists", new[] { "UserId" });
            DropTable("dbo.SharedPlaylists");
            DropTable("dbo.Users");
            DropTable("dbo.RecentlyPlayeds");
            DropTable("dbo.Songs");
            DropTable("dbo.PlaylistSongs");
            DropTable("dbo.Playlists");
        }
    }
}

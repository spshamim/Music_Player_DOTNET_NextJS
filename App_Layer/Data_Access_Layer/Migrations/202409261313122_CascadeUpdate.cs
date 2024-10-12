namespace Data_Access_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlaylistSongs", "PlaylistId", "dbo.Playlists");
            DropForeignKey("dbo.PlaylistSongs", "SongId", "dbo.Songs");
            AddForeignKey("dbo.PlaylistSongs", "PlaylistId", "dbo.Playlists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlaylistSongs", "SongId", "dbo.Songs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaylistSongs", "SongId", "dbo.Songs");
            DropForeignKey("dbo.PlaylistSongs", "PlaylistId", "dbo.Playlists");
            AddForeignKey("dbo.PlaylistSongs", "SongId", "dbo.Songs", "Id");
            AddForeignKey("dbo.PlaylistSongs", "PlaylistId", "dbo.Playlists", "Id");
        }
    }
}

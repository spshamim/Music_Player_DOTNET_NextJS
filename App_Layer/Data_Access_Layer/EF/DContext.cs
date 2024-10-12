using Data_Access_Layer.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EF
{
    public class DContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SharedPlaylist> SharedPlaylists { get; set; }
        public DbSet<RecentlyPlayed> RecentlyPlayed { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Token> Tokens { get; set; }

        // cascade problem solution

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable cascade delete for Playlist -> User relationship
            modelBuilder.Entity<Playlist>()
                .HasRequired(p => p.User)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);

            // Disable cascade delete for SharedPlaylist -> User relationship
            modelBuilder.Entity<SharedPlaylist>()
                .HasRequired(sp => sp.User)
                .WithMany(u => u.SharedPlaylists)
                .HasForeignKey(sp => sp.UserId)
                .WillCascadeOnDelete(false);

            // Disable cascade delete for PlaylistSong -> Playlist relationship
            modelBuilder.Entity<PlaylistSong>()
                .HasRequired(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSongs)
                .HasForeignKey(ps => ps.PlaylistId)
                .WillCascadeOnDelete(true); // when playlist is deleted, playlist songs are deleted

            // Disable cascade delete for PlaylistSong -> Song relationship
            modelBuilder.Entity<PlaylistSong>()
                .HasRequired(ps => ps.Song)
                .WithMany(s => s.PlaylistSongs)
                .HasForeignKey(ps => ps.SongId)
                .WillCascadeOnDelete(true); // when song is deleted, playlist songs are deleted

            // Disable cascade delete for RecentlyPlayed -> User relationship
            modelBuilder.Entity<RecentlyPlayed>()
                .HasRequired(rp => rp.User)
                .WithMany(u => u.RecentlyPlayed)
                .HasForeignKey(rp => rp.UserId)
                .WillCascadeOnDelete(false);

            // Disable cascade delete for RecentlyPlayed -> Song relationship
            modelBuilder.Entity<RecentlyPlayed>()
                .HasRequired(rp => rp.Song)
                .WithMany(s => s.RecentlyPlayed)
                .HasForeignKey(rp => rp.SongId)
                .WillCascadeOnDelete(false);

            // Token and User (with cascade delete)
            modelBuilder.Entity<Token>()
                .HasRequired(t => t.User)
                .WithOptional(u => u.Token)
                .WillCascadeOnDelete(true);
        }
    }
}

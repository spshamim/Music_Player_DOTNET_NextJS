using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class PlaylistSongRepo : Repo, IRepo2<PlaylistSong, string, PlaylistSong>, IShuffleRepeat<PlaylistSong, string>, IViewAllSWithPcs<PlaylistSong>
    {
        public PlaylistSong Create(PlaylistSong obj)
        {
            db.PlaylistSongs.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public List<PlaylistSong> GetByPName(string name)
        {
            return db.PlaylistSongs
                .Where(x => x.Playlist.Name.Contains(name))
                .Include(x => x.Playlist)
                .Include(x => x.Playlist.User) // playlist belongs to which user
                .Include(x => x.Song)
                .ToList();
        }
        public List<PlaylistSong> Get()
        {
            return db.PlaylistSongs.OrderBy(x => x.Playlist.Id).ToList();
        }

        public List<PlaylistSong> GetBySTitle(string title)
        {
            return db.PlaylistSongs
                .Where(x => x.Song.Title.Contains(title))
                .Include(x => x.Playlist) 
                .Include(x => x.Playlist.User) // playlist belongs to which user
                .Include(x => x.Song) // song belongs to which playlist
                .ToList();
        }

        public List<PlaylistSong> GetShuffledSongs(string playlistName)
        {
            var playlistSongs = db.PlaylistSongs
                .Where(x => x.Playlist.Name.Equals(playlistName))
                .Include(x => x.Playlist)
                .Include(x => x.Song) // song belongs to which playlist
                .ToList();

            return Shuffle(playlistSongs);
        }

        private List<PlaylistSong> Shuffle(List<PlaylistSong> plsongs)
        {
            Random rng = new Random();
            int n = plsongs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                PlaylistSong value = plsongs[k];
                plsongs[k] = plsongs[n];
                plsongs[n] = value;
            }
            return plsongs;

            // Fisher-Yates shuffle algorithm, efficiently handle order of items in a list
        }
        public PlaylistSong GetNextSong(string playlistName, int currentSongId, bool repeatSong, bool repeatPlaylist)
        {
            var playlistSongs = db.PlaylistSongs
                .Where(x => x.Playlist.Name.Equals(playlistName))
                .Include(x => x.Song)
                .OrderBy(x => x.Id)
                .ToList();

            int currentIndex = playlistSongs.FindIndex(ps => ps.SongId == currentSongId);

            if (repeatSong)
            {
                return playlistSongs[currentIndex]; // If repeat song is enabled, return the same song
            }

            if (currentIndex == playlistSongs.Count - 1) // if the current song is the last song
            {
                return repeatPlaylist ? playlistSongs[0] : null; // If repeat playlist, return the first song, else null
            }
  
            return playlistSongs[currentIndex + 1]; // Return the next song if no repeat working
        }
    }
}

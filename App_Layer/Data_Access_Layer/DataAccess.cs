using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class DataAccess
    {
        public static IRepo<User, int, User> UserData()
        {
            return new UserRepo();
        }
        public static IRepo<Song, int, Song> SongData()
        {
            return new SongRepo();
        }
        public static IRepo<Playlist, int, Playlist> PlaylistData()
        {
            return new PlaylistRepo();
        }
        public static IRepo<RecentlyPlayed, int, RecentlyPlayed> RecentPlayedData()
        {
            return new RecentlyPlayedRepo();
        }
        public static IRepo2<PlaylistSong, string, PlaylistSong> PlaylistSongData()
        {
            return new PlaylistSongRepo();
        }
        public static IRepo2<SharedPlaylist, string, SharedPlaylist> SharedPlaylistData()
        {
            return new SharedPlaylistRepo();
        }
        public static IAuth AuthData()
        {
            return new UserRepo();
        }
        public static ISongLyrics SongLyricsData()
        {
            return new SongRepo();
        }
        public static IRepo<Token, string, Token> TokenData()
        {
            return new TokenRepo();
        }
        public static IShuffleRepeat<PlaylistSong, string> ShuffleRepeatData()
        {
            return new PlaylistSongRepo();
        }
        public static IViewAllSWithPcs<PlaylistSong> ViewAllSWithPcsData()
        {
            return new PlaylistSongRepo();
        }
    }
}

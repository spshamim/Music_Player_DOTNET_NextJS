using AutoMapper;
using Data_Access_Layer;
using Data_Access_Layer.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.DTOs.PlaylistDTO;
using static Business_Layer.DTOs.PlaylistSongDTO;
using static Business_Layer.DTOs.SongDTO;
using static Business_Layer.DTOs.UserDTO;

namespace Business_Layer.Services
{
    public class PlaylistSongService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PlaylistSong, GetPlaylistSongDTO>();
                cfg.CreateMap<AddSongToPlaylistSongDTO, PlaylistSong>();
                cfg.CreateMap<Song, GetSongDTO>();
                cfg.CreateMap<Playlist, GetPlaylistDTO>();
                cfg.CreateMap<User, GetUserDTO>();
            });
            return new Mapper(config);
        }

        public static bool Create(AddSongToPlaylistSongDTO obj)
        {
            var data = GetMapper().Map<PlaylistSong>(obj);
            return DataAccess.PlaylistSongData().Create(data) != null;
        }

        public static List<GetPlaylistSongDTO> GetWithNav(string name)
        {
            var data = DataAccess.PlaylistSongData().GetByPName(name);
            return GetMapper().Map<List<GetPlaylistSongDTO>>(data);
        }

        public static List<GetPlaylistSongDTO> GetWithNav2(string name)
        {
            var data = DataAccess.PlaylistSongData().GetBySTitle(name);
            return GetMapper().Map<List<GetPlaylistSongDTO>>(data);
        }

        public static List<GetPlaylistSongDTO> GetShuffledSongs(string playlistName)
        {
            var data = DataAccess.ShuffleRepeatData().GetShuffledSongs(playlistName);
            return GetMapper().Map<List<GetPlaylistSongDTO>>(data);
        }
        public static GetPlaylistSongDTO GetNextSong(string playlistName, int currentSongId, bool repeatSong, bool repeatPlaylist)
        {
            var data = DataAccess.ShuffleRepeatData().GetNextSong(playlistName, currentSongId, repeatSong, repeatPlaylist);
            return GetMapper().Map<GetPlaylistSongDTO>(data);
        }

        public static List<GetPlaylistSongDTO> Get()
        {
            var data = DataAccess.ViewAllSWithPcsData().Get();
            return GetMapper().Map<List<GetPlaylistSongDTO>>(data);
        }
    }
}

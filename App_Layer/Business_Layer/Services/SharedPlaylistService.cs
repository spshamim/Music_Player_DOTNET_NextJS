using AutoMapper;
using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.DTOs.PlaylistSongDTO;
using static Business_Layer.DTOs.SharedPlaylistDTO;
using static Business_Layer.DTOs.UserDTO;
using static Business_Layer.DTOs.PlaylistDTO;

namespace Business_Layer.Services
{
    public class SharedPlaylistService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SharedPlaylist, GetSharedPlaylistDTO>();
                cfg.CreateMap<AddSharePlaylistDTO, SharedPlaylist>();
                cfg.CreateMap<User, GetUserDTO>();
                cfg.CreateMap<Playlist, GetPlaylistDTO>();
            });
            return new Mapper(config);
        }

        public static bool Create(AddSharePlaylistDTO obj)
        {
            var data = GetMapper().Map<SharedPlaylist>(obj);
            return DataAccess.SharedPlaylistData().Create(data) != null;
        }
        public static List<GetSharedPlaylistDTO> Get()
        {
            var data = DataAccess.SharedPlaylistData().Get();
            return GetMapper().Map<List<GetSharedPlaylistDTO>>(data);
        }

        public static List<GetSharedPlaylistDTO> GetWithNav(string name)
        {
            var data = DataAccess.SharedPlaylistData().GetByPName(name);
            return GetMapper().Map<List<GetSharedPlaylistDTO>>(data);
        }
        public static List<GetSharedPlaylistDTO> GetWithNav2(string name)
        {
            var data = DataAccess.SharedPlaylistData().GetBySTitle(name);
            return GetMapper().Map<List<GetSharedPlaylistDTO>>(data);
        }
    }
}

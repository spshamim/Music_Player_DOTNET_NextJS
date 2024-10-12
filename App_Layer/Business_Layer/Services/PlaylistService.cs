using AutoMapper;
using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.DTOs.UserDTO;
using static Business_Layer.DTOs.PlaylistDTO;

namespace Business_Layer.Services
{
    public class PlaylistService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Playlist, GetPlaylistDTO>();
                cfg.CreateMap<CreatePlaylistDTO, Playlist>();
                cfg.CreateMap<UpdatePlaylistDTO, Playlist>();
                cfg.CreateMap<User, GetUserDTO>();
            });
            return new Mapper(config);
        }
        public static bool Create(CreatePlaylistDTO obj)
        {
            var data = GetMapper().Map<Playlist>(obj);
            return DataAccess.PlaylistData().Create(data) != null;
        }
        public static List<GetPlaylistDTO> Get()
        {
            var data = DataAccess.PlaylistData().Get();
            return GetMapper().Map<List<GetPlaylistDTO>>(data);
        }
        public static List<GetPlaylistDTO> GetByName(string name)
        {
            var data = DataAccess.PlaylistData().GetByString(name);
            return GetMapper().Map<List<GetPlaylistDTO>>(data);
        }
        public static GetPlaylistDTO Get(int id)
        {
            var data = DataAccess.PlaylistData().Get(id);
            return GetMapper().Map<GetPlaylistDTO>(data);
        }
        public static bool Update(UpdatePlaylistDTO obj)
        {
            var data = GetMapper().Map<Playlist>(obj);
            return DataAccess.PlaylistData().Update(data) != null;
        }
        public static bool Delete(int id)
        {
            return DataAccess.PlaylistData().Delete(id);
        }
    }
}

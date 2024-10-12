using AutoMapper;
using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.DTOs.PlaylistDTO;
using static Business_Layer.DTOs.RecentlyPlayedDTO;
using static Business_Layer.DTOs.UserDTO;
using static Business_Layer.DTOs.SongDTO;

namespace Business_Layer.Services
{
    public class RecentlyPlayedService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RecentlyPlayed, GetRecentlyPlayedDTO>();
                cfg.CreateMap<AddRecentlyPlayedDTO, RecentlyPlayed>();
                cfg.CreateMap<UpdateRecentlyPlayedDTO, RecentlyPlayed>();
                cfg.CreateMap<User, GetUserDTO>();
                cfg.CreateMap<Song, GetSongDTO>();
            });
            return new Mapper(config);
        }
        public static bool Create(AddRecentlyPlayedDTO obj)
        {
            var data = GetMapper().Map<RecentlyPlayed>(obj);
            return DataAccess.RecentPlayedData().Create(data) != null;
        }
        public static List<GetRecentlyPlayedDTO> Get()
        {
            var data = DataAccess.RecentPlayedData().Get();
            return GetMapper().Map<List<GetRecentlyPlayedDTO>>(data);
        }
        public static List<GetRecentlyPlayedDTO> GetByN(string name)
        {
            var data = DataAccess.RecentPlayedData().GetByString(name);
            return GetMapper().Map<List<GetRecentlyPlayedDTO>>(data);
        }
        public static GetRecentlyPlayedDTO Get(int id)
        {
            var data = DataAccess.RecentPlayedData().Get(id);
            return GetMapper().Map<GetRecentlyPlayedDTO>(data);
        }
        public static bool Update(UpdateRecentlyPlayedDTO obj)
        {
            var data = GetMapper().Map<RecentlyPlayed>(obj);
            return DataAccess.RecentPlayedData().Update(data) != null;
        }
        public static bool Delete(int id)
        {
            return DataAccess.RecentPlayedData().Delete(id);
        }
    }
}

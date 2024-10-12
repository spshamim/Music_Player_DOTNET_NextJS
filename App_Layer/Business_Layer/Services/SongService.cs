using AutoMapper;
using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.DTOs.SongDTO;

namespace Business_Layer.Services
{
    public class SongService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Song, GetSongDTO>();
                cfg.CreateMap<CreateSongDTO, Song>();
                cfg.CreateMap<UpdateSongDTO, Song>();
            });
            return new Mapper(config);
        }
        public static bool Create(CreateSongDTO obj)
        {
            var data = GetMapper().Map<Song>(obj);
            return DataAccess.SongData().Create(data) != null;
        }
        public static List<GetSongDTO> Get()
        {
            var data = DataAccess.SongData().Get();
            return GetMapper().Map<List<GetSongDTO>>(data);
        }
        public static GetSongDTO Get(int id)
        {
            var data = DataAccess.SongData().Get(id);
            return GetMapper().Map<GetSongDTO>(data);
        }
        public static bool Update(UpdateSongDTO obj)
        {
            var data = GetMapper().Map<Song>(obj);
            return DataAccess.SongData().Update(data) != null;
        }
        public static bool Delete(int id)
        {
            return DataAccess.SongData().Delete(id);
        }

        public static string GetLyrics(string songName)
        {
            return DataAccess.SongLyricsData().GetLyrics(songName);
        }

        public static List<GetSongDTO> GetByString(string name)
        {
            var data = DataAccess.SongData().GetByString(name);
            return GetMapper().Map<List<GetSongDTO>>(data);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs
{
    public class RecentlyPlayedDTO
    {
        public class GetRecentlyPlayedDTO
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int SongId { get; set; }
            public DateTime PlayedAt { get; set; }

            public virtual UserDTO.GetUserDTO User { get; set; }
            public virtual SongDTO.GetSongDTO Song { get; set; }
        }

        public class AddRecentlyPlayedDTO
        {
            public int Id { get; set; }
            public DateTime PlayedAt { get; set; }
            public int UserId { get; set; }
            public int SongId { get; set; }
        }
        public class UpdateRecentlyPlayedDTO
        {
            public int Id { get; set; }
            public DateTime PlayedAt { get; set; }
            public int UserId { get; set; }
            public int SongId { get; set; }
        }

    }
}

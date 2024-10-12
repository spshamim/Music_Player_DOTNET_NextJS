using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs
{
    public class PlaylistSongDTO
    {
        public class GetPlaylistSongDTO
        {
            public int Id { get; set; }
            public int PlaylistId { get; set; }
            public int SongId { get; set; }

            // navigation properties
            public virtual PlaylistDTO.GetPlaylistDTO Playlist { get; set; }
            public virtual SongDTO.GetSongDTO Song { get; set; }
            public virtual UserDTO.GetUserDTO User { get; set; }
        }

        public class AddSongToPlaylistSongDTO
        {
            public int Id { get; set; }
            public int PlaylistId { get; set; }
            public int SongId { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs
{
    public class SharedPlaylistDTO
    {
        public class GetSharedPlaylistDTO
        {
            public int Id { get; set; }
            public int PlaylistId { get; set; }
            public int UserId { get; set; }
            public virtual PlaylistDTO.GetPlaylistDTO Playlist { get; set; }
            public virtual UserDTO.GetUserDTO User { get; set; }
        }

        public class AddSharePlaylistDTO
        {
            public int PlaylistId { get; set; }
            public int UserId { get; set; }
        }

    }
}

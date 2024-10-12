using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs
{
    public class PlaylistDTO
    {
        public class GetPlaylistDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
            // nav
            public virtual UserDTO.GetUserDTO User { get; set; }
        }

        public class CreatePlaylistDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int UserId { get; set; }
        }

        public class UpdatePlaylistDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}

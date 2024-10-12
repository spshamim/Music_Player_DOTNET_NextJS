using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EF.TableModels
{
    public class SharedPlaylist
    {
        [Required]
        public int Id { get; set; }
        
        // navigation properties
        public virtual Playlist Playlist { get; set; }
        [ForeignKey("Playlist")]
        public int PlaylistId { get; set; }

        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}

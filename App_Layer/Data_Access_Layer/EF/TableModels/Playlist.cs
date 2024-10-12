using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EF.TableModels
{
    public class Playlist
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
        public virtual ICollection<SharedPlaylist> SharedPlaylists { get; set; }

        public Playlist()
        {
            PlaylistSongs = new List<PlaylistSong>();
            SharedPlaylists = new List<SharedPlaylist>();
        }
    }
}

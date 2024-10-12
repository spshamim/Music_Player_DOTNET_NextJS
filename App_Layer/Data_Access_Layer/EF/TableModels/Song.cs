using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EF.TableModels
{
    public class Song
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Artist { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Album { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Lyrics { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan Duration { get; set; }

        // Navigation properties
        public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; }
        public virtual ICollection<RecentlyPlayed> RecentlyPlayed { get; set; }

        public Song()
        {
            PlaylistSongs = new List<PlaylistSong>();
            RecentlyPlayed = new List<RecentlyPlayed>();
        }
    }
}

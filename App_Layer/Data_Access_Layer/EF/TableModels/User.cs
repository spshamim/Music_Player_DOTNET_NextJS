using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EF.TableModels
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string ResetToken { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? resetTokenExpires { get; set; }

        // Navigation properties
        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual ICollection<SharedPlaylist> SharedPlaylists { get; set; }
        public virtual ICollection<RecentlyPlayed> RecentlyPlayed { get; set; }

        public virtual Token Token { get; set; }
        public User()
        {
            Playlists = new List<Playlist>();
            SharedPlaylists = new List<SharedPlaylist>();
            RecentlyPlayed = new List<RecentlyPlayed>();
        }
    }
}

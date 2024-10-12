using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EF.TableModels
{
    public class RecentlyPlayed
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime PlayedAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Song Song { get; set; }
        [ForeignKey("Song")]
        public int SongId { get; set; }
    }
}

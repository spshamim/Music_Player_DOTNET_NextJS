using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs
{
    public class SongDTO
    {
        public class GetSongDTO
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Album { get; set; }
            public string Lyrics { get; set; }
            public TimeSpan Duration { get; set; }
        }

        public class CreateSongDTO
        {
            public int Id{ get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Album { get; set; }
            public string Lyrics { get; set; }
            public TimeSpan Duration { get; set; }
        }

        public class UpdateSongDTO
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Album { get; set; }
            public string Lyrics { get; set; }
            public TimeSpan Duration { get; set; }
        }
    }
}

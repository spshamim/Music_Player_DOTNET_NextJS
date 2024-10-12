using Data_Access_Layer.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IShuffleRepeat<CLASS, ID>
    {
        List<CLASS> GetShuffledSongs(ID playlistName);
        CLASS GetNextSong(ID playlistName, int currentSongId, bool repeatSong, bool repeatPlaylist);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IRepo2<CLASS, ID, RET>
    {
        RET Create(CLASS obj);
        List<CLASS> GetByPName(ID id); // search by playlist ID, show all songs under the playlist
        List<CLASS> GetBySTitle(ID id); // search by song ID

        List<CLASS> Get();

        // if playlist got deleted, playlistsong entry will also be deleted
        // if songs got deleted, playlistsong entry will also be deleted
    }
}

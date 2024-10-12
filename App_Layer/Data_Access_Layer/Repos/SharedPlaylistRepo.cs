using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class SharedPlaylistRepo : Repo, IRepo2<SharedPlaylist, string, SharedPlaylist>
    {
        public SharedPlaylist Create(SharedPlaylist obj)
        {
            db.SharedPlaylists.Add(obj);
            db.SaveChanges();
            return obj;

            //if (db.SharedPlaylists
            //    .Where(x => x.Playlist.UserId.Equals(obj.User.Id))
            //    .Include(x => x.Playlist)
            //    .Include(x => x.User)
            //    .Any()
            //    )
            //{
            //    throw new Exception("Playlist can't be shared with the owner. Try sharing with different users.");
            //}
            //else if (!db.SharedPlaylists
            //    .Where(u => u.User.Id.Equals(obj.User.Id))
            //    .Include(u => u.User)
            //    .Include(path => path.Playlist)
            //    .Any()
            //    )
            //{
            //    throw new Exception("User with the specified is not found.");
            //}
        }

        public List<SharedPlaylist> Get()
        {
            return db.SharedPlaylists.ToList();
        }

        public List<SharedPlaylist> GetByPName(string pname)
        {
            return db.SharedPlaylists
                .Where(x => x.Playlist.Name.Contains(pname)) // search with playlist name
                .Include(x => x.Playlist) // user
                .ToList();
        }

        public List<SharedPlaylist> GetBySTitle(string uname) // want to see which playlist is shared by this user
        {
            return db.SharedPlaylists
                .Where(x => x.User.Username.Contains(uname))
                .Include(x => x.Playlist)
                .ToList();
        }
    }
}
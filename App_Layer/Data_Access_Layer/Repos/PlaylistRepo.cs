using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class PlaylistRepo : Repo, IRepo<Playlist, int, Playlist>
    {
        public Playlist Create(Playlist obj)
        {
            Playlist playlist = new Playlist
            {
                Name = obj.Name,
                Description = obj.Description,
                CreatedAt = DateTime.Now,
                UserId = obj.UserId
            };
            db.Playlists.Add(playlist);
            db.SaveChanges();
            return playlist;
        }

        public bool Delete(int id)
        {
            var explaylist = Get(id);
            db.Playlists.Remove(explaylist);
            return db.SaveChanges() > 0;
        }

        public List<Playlist> Get()
        {
            return db.Playlists.ToList();
        }

        public Playlist Get(int id)
        {
            return db.Playlists.Find(id);
        }

        public List<Playlist> GetByString(string name)
        {
            return db.Playlists.Where(x => x.Name.Contains(name)).ToList();
        }

        public Playlist Update(Playlist obj)
        {
            var exobj = Get(obj.Id);
            if (exobj == null)
            {
                throw new Exception("Playlist not found");
            }

            bool isUpdated = false;

            if (obj.Name != null)
            {
                exobj.Name = obj.Name;
                isUpdated = true;
            }

            if (obj.Description != null)
            {
                exobj.Description = obj.Description;
                isUpdated = true;
            }

            if (isUpdated)
            {
                exobj.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }

            return exobj;
        }
    }
}

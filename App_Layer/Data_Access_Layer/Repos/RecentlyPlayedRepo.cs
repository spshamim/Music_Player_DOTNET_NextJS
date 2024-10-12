using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class RecentlyPlayedRepo : Repo, IRepo<RecentlyPlayed, int, RecentlyPlayed>
    {
        public RecentlyPlayed Create(RecentlyPlayed obj)
        {
            db.RecentlyPlayed.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var exobj = Get(id);
            db.RecentlyPlayed.Remove(exobj);
            return db.SaveChanges() > 0;
        }

        public List<RecentlyPlayed> Get()
        {
            return db.RecentlyPlayed.ToList();
        }

        public RecentlyPlayed Get(int id)
        {
            return db.RecentlyPlayed.Find(id);
        }

        public List<RecentlyPlayed> GetByString(string name)
        {
            return db.RecentlyPlayed.Where(x => x.User.Username.Contains(name)).ToList();
        }

        public RecentlyPlayed Update(RecentlyPlayed obj)
        {
            var exobj = Get(obj.Id);
            bool isUpdated = false;

            if (obj.SongId != 0)
            {
                exobj.SongId = obj.SongId;
                isUpdated = true;
            }

            if (obj.UserId != 0)
            {
                exobj.UserId = obj.UserId;
                isUpdated = true;
            }

            if (obj.PlayedAt != null)
            {
                exobj.PlayedAt = obj.PlayedAt;
                isUpdated = true;
            }
            
            if (isUpdated)
            {
                db.SaveChanges();
                return exobj;
            }
            return exobj;
        }
    }
}

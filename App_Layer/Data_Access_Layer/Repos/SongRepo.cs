using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class SongRepo : Repo, IRepo<Song, int, Song>, ISongLyrics
    {
        public Song Create(Song obj)
        {
            db.Songs.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var exsong = Get(id);
            db.Songs.Remove(exsong);
            return db.SaveChanges() > 0;
        }

        public List<Song> Get()
        {
            return db.Songs.ToList();
        }

        public Song Get(int id)
        {
            return db.Songs.Find(id);
        }

        public List<Song> GetByString(string name)
        {
            return db.Songs.Where(x => x.Title.Contains(name)).ToList();
        }

        public string GetLyrics(string songName)
        {
            var song = db.Songs.SingleOrDefault(x => x.Title.Equals(songName));
            return song.Lyrics;
        }

        public Song Update(Song obj)
        {
            var exobj = Get(obj.Id);
            bool isUpdated = false;

            if(obj.Title != null)
            {
                exobj.Title = obj.Title;
                isUpdated = true;
            }

            if(obj.Artist != null)
            {
                exobj.Artist = obj.Artist;
                isUpdated = true;
            }

            if (obj.Album != null)
            {
                exobj.Album = obj.Album;
                isUpdated = true;
            }

            if (obj.Lyrics != null)
            {
                exobj.Lyrics = obj.Lyrics;
                isUpdated = true;
            }

            if (obj.Duration != null)
            {
                exobj.Duration = obj.Duration;
                isUpdated = true;
            }
            
            if (isUpdated)
            {
                db.SaveChanges();
            }

            return exobj;
        }
    }
}

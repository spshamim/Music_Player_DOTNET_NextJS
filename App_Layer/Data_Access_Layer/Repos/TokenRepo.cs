using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repos
{
    internal class TokenRepo : Repo, IRepo<Token, string, Token>
    {
        public Token Create(Token obj)
        {
            var existingToken = db.Tokens.SingleOrDefault(t => t.Id == obj.Id);

            if (existingToken != null)
            {
                // If token exists, update the Key and CreatedAt
                existingToken.Key = obj.Key;
                existingToken.CreatedAt = obj.CreatedAt;
                existingToken.ExpiredAt = obj.ExpiredAt;
            }
            else
            {
                db.Tokens.Add(obj);
            }

            db.SaveChanges();
            return obj;
        }

        public bool Delete(string id)
        {
            var exobj = Get(id);
            db.Tokens.Remove(exobj);
            return db.SaveChanges()>0;
        }

        public List<Token> Get()
        {
            return db.Tokens.ToList();
        }

        public Token Get(string id)
        {
            return db.Tokens.SingleOrDefault(t => t.Key.Equals(id));
        }

        public List<Token> GetByString(string name)
        {
            throw new NotImplementedException();
        }

        public Token Update(Token obj)
        {
            var exobj = Get(obj.Key);
            exobj.ExpiredAt = obj.ExpiredAt;
            db.SaveChanges();
            return exobj;
        }
    }
}

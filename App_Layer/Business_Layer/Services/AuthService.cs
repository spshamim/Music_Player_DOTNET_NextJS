using AutoMapper;
using Business_Layer.DTOs;
using Data_Access_Layer;
using Data_Access_Layer.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class AuthService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Token, TokenDTO>();

            });
            return new Mapper(config);
        }

        public static TokenDTO Authenticate(string uname, string pass)
        {
            var data = DataAccess.AuthData().Authenticate(uname, pass);
            if (data!=null)
            {
                Token t = new Token();
                t.CreatedAt = DateTime.Now;
                t.Key = Guid.NewGuid().ToString();
                t.Id = data.Id;
                var token = DataAccess.TokenData().Create(t);
                return GetMapper().Map<TokenDTO>(token);
            }
            return null;
        }
        public static bool LogoutToken(string key)
        {
            var existingToken = DataAccess.TokenData().Get(key);
            if (existingToken != null)
            {
                existingToken.ExpiredAt = DateTime.Now;
                var ret = DataAccess.TokenData().Update(existingToken);
                return ret != null;
            }
            return false;
        }

        public static bool IsTokenValid(string key)
        {
            var token = DataAccess.TokenData().Get(key);
            if (token != null && (token.ExpiredAt == null || token.ExpiredAt > DateTime.Now)) return true;
            return false;
        }
    }
}

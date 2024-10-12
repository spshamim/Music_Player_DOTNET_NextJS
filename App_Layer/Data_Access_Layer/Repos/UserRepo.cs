using Data_Access_Layer.EF.TableModels;
using Data_Access_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Data_Access_Layer.Repos
{
    internal class UserRepo : Repo, IRepo<User, int, User>, IAuth
    {
        public User Create(User obj)
        {
            User user = new User();
            user.Username = obj.Username;
            user.Email = obj.Email;
            user.Password = HashPassword(obj.Password);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = null;
            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var exuser = Get(id);
            db.Users.Remove(exuser);
            return db.SaveChanges() > 0;
        }

        public List<User> Get()
        {
            return db.Users.ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public User Update(User obj)
        {
            var exobj = Get(obj.Id);
            if (exobj == null)
            {
                throw new Exception("User not found");
            }

            bool isUpdated = false;

            if (obj.Username != null)
            {
                exobj.Username = obj.Username;
                isUpdated = true;
            }

            if (obj.Email != null)
            {
                exobj.Email = obj.Email;
                isUpdated = true;
            }

            if(obj.Password != null)
            {
                var HashedPassword = HashPassword(obj.Password);
                exobj.Password = HashedPassword;
                isUpdated = true;
            }

            if (isUpdated)
            {
                exobj.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }
            return exobj;
        }

        public User Authenticate(string uname, string pass)
        {
            var user = db.Users.SingleOrDefault(u => u.Username.Equals(uname));
            if (user != null && VerifyPassword(pass, user.Password))
            {
                return user; // If password matches, return the user
            }
            return null; // If password doesn't match, return null
        }

        // password hashing
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }

        public List<User> GetByString(string name)
        {
            return db.Users.Where(x => x.Username.Contains(name)).ToList();
        }

        public User generateResetCode(string email, string code)
        {
            var user = db.Users.SingleOrDefault(u => u.Email.Equals(email));
            if (user != null && code != null)
            {
                user.ResetToken = code;
                user.resetTokenExpires = DateTime.Now.AddHours(1);
                db.SaveChanges();
                return user;
            }
            return null;
        }

        public User ResetPasswordEmail(string email, string randomCode, string newPassword)
        {
            var user = db.Users.SingleOrDefault(u => u.ResetToken.Equals(randomCode) && u.Email.Equals(email));

            if (user == null)
                return null;

            if (user.resetTokenExpires < DateTime.Now)
                return null;

            user.Password = HashPassword(newPassword);
            user.ResetToken = null;
            user.resetTokenExpires = null;

            db.SaveChanges();
            return user;
        }
    }
}

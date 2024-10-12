using AutoMapper;
using Business_Layer.Services.Email;
using Business_Layer.Services.Utility;
using Data_Access_Layer;
using Data_Access_Layer.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.DTOs.UserDTO;

namespace Business_Layer.Services
{
    public class UserService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, GetUserDTO>();
                cfg.CreateMap<CreateUserDTO, User>();
                cfg.CreateMap<UpdateUserDTO, User>();
            });
            return new Mapper(config);
        }
        public static bool Create(CreateUserDTO obj)
        {
            var data = GetMapper().Map<User>(obj);
            var result = DataAccess.UserData().Create(data);

            if (result != null)
            {
                string templatePath = "D:\\Testing\\App_Layer\\Business_Layer\\Services\\Email\\Templates\\AccountOpen.html";

                var placeholders = new Dictionary<string, string>
                {
                    { "{{userName}}", obj.Username },
                    { "{{email}}", obj.Email },
                    { "{{registrationDate}}", DateTime.Now.ToString("MMMM dd, yyyy") }
                };

                EmailService.SendEmail("email@gmail.com", "Registration Successful", templatePath, placeholders);

                return true;
            }
            return false;
        }
        public static List<GetUserDTO> Get()
        {
            var data = DataAccess.UserData().Get();
            return GetMapper().Map<List<GetUserDTO>>(data);
        }
        public static GetUserDTO Get(int id)
        {
            var data = DataAccess.UserData().Get(id);
            return GetMapper().Map<GetUserDTO>(data);
        }
        public static bool Update(UpdateUserDTO obj)
        {
            var data = GetMapper().Map<User>(obj);
            return DataAccess.UserData().Update(data) != null;
        }
        public static bool Delete(int id)
        {
            return DataAccess.UserData().Delete(id);
        }
        public static List<GetUserDTO> GetByN(string name)
        {
            var data = DataAccess.UserData().GetByString(name);
            return GetMapper().Map<List<GetUserDTO>>(data);
        }

        public static bool GenerateResetCode(string email)
        {

            var randomCode = CryptoUses.GenerateRandomCode(20);

            var data = DataAccess.AuthData().generateResetCode(email, randomCode);

            if (data != null)
            {
                string resetLink = $"http://localhost:3000/auth/reset-password?token={randomCode}";

                string templatePath = "D:\\Testing\\App_Layer\\Business_Layer\\Services\\Email\\Templates\\PasswordReset.html";
                var placeholders = new Dictionary<string, string>
                {
                    { "{{resetURL}}", resetLink },
                    { "{{userName}}", data.Username }
                };

                EmailService.SendEmail("email@gmail.com", "Reset Password", templatePath, placeholders);

                return true;
            }

            return false;
        }

        public static bool ResetPasswordEmail(string email,string password, string token)
        {
            var data = DataAccess.AuthData().ResetPasswordEmail(email, token, password);

            if (data != null)
            {
                string templatePath = "D:\\Testing\\App_Layer\\Business_Layer\\Services\\Email\\Templates\\ResetSuccess.html";
                var placeholders = new Dictionary<string, string>
                {
                    { "{userName}", data.Username }
                };

                EmailService.SendEmail("email@gmail.com", "Password Reset Successfull", templatePath, placeholders);

                return true;
            }

            return false;
        }
    }
}

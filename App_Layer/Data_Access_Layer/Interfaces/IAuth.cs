using Data_Access_Layer.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IAuth
    {
        User Authenticate(string uname, string pass);
        User generateResetCode(string email, string code);
        User ResetPasswordEmail(string email, string randomCode, string newPassword);
    }
}

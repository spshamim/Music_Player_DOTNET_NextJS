using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.DTOs
{
    public class UserDTO
    {
        public class GetUserDTO
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }

        public class CreateUserDTO
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UpdateUserDTO
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}

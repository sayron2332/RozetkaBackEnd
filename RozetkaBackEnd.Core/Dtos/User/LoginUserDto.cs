using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Dtos.User
{
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

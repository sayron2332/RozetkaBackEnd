﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Dtos.User
{
    public class EditUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

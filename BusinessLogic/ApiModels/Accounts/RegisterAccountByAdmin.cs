﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApiModels.Accounts
{
    public class RegisterAccountByAdmin
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Role{ get; set; }
    }
}

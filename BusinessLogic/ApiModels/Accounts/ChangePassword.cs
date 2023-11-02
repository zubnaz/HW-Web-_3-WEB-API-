using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApiModels.Accounts
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string repetitionNewPassword { get; set; }
    }
}

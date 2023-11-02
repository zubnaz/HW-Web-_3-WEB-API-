using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Data.Entitys
{
    public class User:IdentityUser
    {
        public DateTime BirthDate { get; set; }
    }
}

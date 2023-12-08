
using BusinessLogic.ApiModels.Autos;
using Microsoft.AspNetCore.Identity;


namespace BusinessLogic.Data.Entitys
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
    }
}

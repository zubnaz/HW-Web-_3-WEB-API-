using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IJwtServices
    {
        IEnumerable<Claim> GetClaims(IdentityUser user);
        string CreateToken(IEnumerable<Claim> claims);
    }
}

using BusinessLogic.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BusinessLogic.Helpers;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration config;

        public JwtServices(IConfiguration config)
        {
            this.config = config;
        }
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var jwtOpts = config.GetSection("JwtOptions").Get<JwtOptions>();
            

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOpts.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtOpts.Lifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };


            return claims;
        }
    }
}

using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Data.Entitys;
using Microsoft.AspNetCore.Identity;
using System.Net;
using BusinessLogic.Dtos;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IJwtServices iJS;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private static string loginUser = string.Empty;
        public AccountServices(IJwtServices iJS,SignInManager<User> signInManager,UserManager<User> userManager)
        {
            this.iJS = iJS;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task ChangePassword(ChangePassword cp)
        {
            if(cp.newPassword!=cp.repetitionNewPassword) { throw new HttpException("Incorrectly entered password!",HttpStatusCode.Conflict);}
            var user = await userManager.FindByNameAsync(cp.Email);
            var result = await userManager.ChangePasswordAsync(user, cp.currentPassword, cp.newPassword);
            if (!result.Succeeded)
            {
                throw new HttpException(string.Join(", ", result.Errors.Select(e => e.Description)), HttpStatusCode.BadRequest);
            }
        }
        public async Task<string> IsAdmin()
        {
            var user = await userManager.FindByNameAsync(loginUser);
            bool isAdmin = await userManager.IsInRoleAsync(user, RolesAccount.Role.Admin.ToString());
            return isAdmin ? "True" : "False";
        }
        public string IsSignIn()
        {
            return loginUser==string.Empty?"False":"True";
        }
        public async Task Exit()
        {
            await signInManager.SignOutAsync();
            loginUser= string.Empty;
        }

        public async Task<LoginResponse> Login(LoginAccount la)
        {
            var user = await userManager.FindByNameAsync(la.EmailAddress);
            if(user == null || !await userManager.CheckPasswordAsync(user,la.Password)) 
                throw new HttpException("Invalid login or password :(", HttpStatusCode.BadRequest);
            await signInManager.SignInAsync(user,true);
            loginUser = la.EmailAddress;
            bool isAdmin = await userManager.IsInRoleAsync(user, RolesAccount.Role.Admin.ToString());

            return new LoginResponse() {
                Token = iJS.CreateToken(iJS.GetClaims(user)),
                isAdmin = isAdmin == true ? "Yes" : "No"
            };


        }

        public async Task Register(RegisterAccount ra = null, RegisterAccountByAdmin raba = null)
        {
            User user = null;
            if (ra != null) { 
            user = new User()
            {
                UserName = ra.EmailAddress,
                Email = ra.EmailAddress,
                PhoneNumber = ra.PhoneNumber,
                BirthDate = ra.Birthdate
                
            };
                userManager.AddToRoleAsync(user,RolesAccount.Role.User.ToString());
            }
            else
            {
                if (await getUser() != null && await userManager.IsInRoleAsync(await getUser(), RolesAccount.Role.Admin.ToString())){
                    user = new User()
                    {
                        UserName = raba.EmailAddress,
                        Email = raba.EmailAddress,
                        PhoneNumber = raba.PhoneNumber,
                        BirthDate = raba.Birthdate
                    };
                    userManager.AddToRoleAsync(user, raba.Role);
                }
                else
                    throw new HttpException("You isn't admin!", HttpStatusCode.Forbidden);
            }
            var result = await userManager.CreateAsync(user, ra == null? raba.Password : ra.Password);
           
            if(!result.Succeeded) {
                throw new HttpException(string.Join(", ", result.Errors.Select(e => e.Description)),HttpStatusCode.BadRequest);
            }
        }
        public async Task<User> getUser()
        {
            return await userManager.FindByNameAsync(loginUser);
        }
    }
}

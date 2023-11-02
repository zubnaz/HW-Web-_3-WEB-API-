using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using DataProject.Data.Entitys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        public AccountServices(SignInManager<User> signInManager,UserManager<User> userManager)
        {
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

        public async Task Exit()
        {
            await signInManager.SignOutAsync();
        }

        public async Task Login(LoginAccount la)
        {
            var user = await userManager.FindByNameAsync(la.EmailAddress);
            if(user == null || !await userManager.CheckPasswordAsync(user,la.Password)) 
                throw new HttpException("Invalid login or password :(", HttpStatusCode.BadRequest);
            await signInManager.SignInAsync(user,true);
           


        }

        public async Task Register(RegisterAccount ra)
        {
            var user = new User()
            {
                UserName = ra.EmailAddress,
                Email = ra.EmailAddress,
                PhoneNumber = ra.PhoneNumber,
                BirthDate = ra.Birthdate
            };
            var result = await userManager.CreateAsync(user,ra.Password);
            if(!result.Succeeded) {
                throw new HttpException(string.Join(", ", result.Errors.Select(e => e.Description)),HttpStatusCode.BadRequest);
            }
        }
    }
}

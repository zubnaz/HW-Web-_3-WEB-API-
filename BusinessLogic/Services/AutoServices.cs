using AutoMapper;
using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Data.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Exceptions;
using System.Net;

namespace BusinessLogic.Services
{
    public class AutoServices : IAutosServices
    {
        public AutoServices(IMapper im,IDataServices<Auto> ids,IAccountServices iAS, UserManager<User> userManager)
        {
            Im = im;
            this.ids = ids;
            this.iAS = iAS;
            this.userManager = userManager;
        }

        private readonly IMapper Im;
        private readonly IDataServices<Auto> ids;
        private readonly IAccountServices iAS;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public async Task Create(CreateAutoModel auto)
        {
            var user = await iAS.getUser();
            //if(user == null || (!await userManager.IsInRoleAsync(user, RolesAccount.Role.Admin.ToString()) && !await userManager.IsInRoleAsync(user, RolesAccount.Role.Moderator.ToString()))) throw new HttpException("You haven't rights!", HttpStatusCode.Forbidden);
            //else  {
                ids.Insert(Im.Map<Auto>(auto));
                await ids.SaveAsync();  
            //}
        }

        public async Task Delete(int id)
        {
            var user = await iAS.getUser();
            if (user == null || !await userManager.IsInRoleAsync(user, RolesAccount.Role.Admin.ToString())) throw new HttpException("You isn't admin!", HttpStatusCode.Forbidden);
            else
            {
                ids.Delete(id);
                await ids.SaveAsync();
            }

        }

        public async Task Edit(EditAutoModel auto)
        {
            var user = await iAS.getUser();
            if (user == null || (!await userManager.IsInRoleAsync(user, RolesAccount.Role.Admin.ToString()) && !await userManager.IsInRoleAsync(user, RolesAccount.Role.Moderator.ToString()))) throw new HttpException("You haven't rights!", HttpStatusCode.Forbidden);
            else
            {
                ids.Update(Im.Map<Auto>(auto));
                await ids.SaveAsync();
            }           
        }

        public Task<List<AutoDtos>> GetAsync()
        {
            return Task.Run(async () => { 
                var autos = await ids.GetAsync(includeProperties: "Color");
                return Im.Map<List<AutoDtos>>(autos); 
            });         
        }

        public AutoDtos? Get(int id)
        {
            return Im.Map<AutoDtos>(ids.GetByID(id, includeProperties: "Color"));
        }

        public List<AutoDtos> Get()
        {
             return Im.Map<List<AutoDtos>>(ids.Get(includeProperties: "Color")); 
        }

        public Task<AutoDtos>? GetAsync(int id)
        {
            return Task.Run(async () => {
                var auto = await ids.GetByIDAsync(id, includeProperties: "Color");
                return Im.Map<AutoDtos>(auto);
            });
            
        }

        public List<AutoDtos> Sort(string type,string by)
        {
            List<AutoDtos> autos;
            if (by =="price")
            {
                if(type =="up") autos = Im.Map<List<AutoDtos>>(ids.Get()).OrderBy(a=>a.Price).ToList();
                else autos = Im.Map<List<AutoDtos>>(ids.Get()).OrderByDescending(a=>a.Price).ToList();
            }
            else if(by == "mark")
            {
                if (type == "up") autos = Im.Map<List<AutoDtos>>(ids.Get()).OrderBy(a => a.Mark).ToList();
                else autos = Im.Map<List<AutoDtos>>(ids.Get()).OrderByDescending(a => a.Mark).ToList();
            }
            else {
                if (type == "up") autos = Im.Map<List<AutoDtos>>(ids.Get()).OrderBy(a => a.Model).ToList();
                else autos = Im.Map<List<AutoDtos>>(ids.Get()).OrderByDescending(a => a.Model).ToList();
            }
            return autos;
        }
    }
}

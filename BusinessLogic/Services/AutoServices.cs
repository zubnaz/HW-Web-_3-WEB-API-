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
using Microsoft.EntityFrameworkCore.Internal;

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

        public List<AutoDtos> Find(string mark, string model, string price)
        {
            List<AutoDtos> autos = Im.Map<List<AutoDtos>>(ids.Get());
            List<AutoDtos> findAutos=new List<AutoDtos>();
           /*List<AutoDtos> Autos_ma = new List<AutoDtos>();
            List<AutoDtos> Autos_mo = new List<AutoDtos>();
            List<AutoDtos> Autos_pr = new List<AutoDtos>();
            foreach(var auto in autos)
            {
                if(auto.Mark == mark) { Autos_ma.Add(auto); }
                if(auto.Model == model) { Autos_mo.Add(auto); }
                if(auto.Price== int.Parse(price)) { Autos_pr.Add(auto); }

                if ((Autos_ma.Find(a => a.Mark == auto.Mark) != null) && (Autos_mo.Find(a => a.Model == auto.Model) != null) && (Autos_pr.Find(a => a.Price == auto.Price) != null))
                    findAutos.Add(auto);
            }*/
            foreach (var auto in autos)
            {
                if (mark != "" && model!=""&&price!="")
                {
                    if(auto.Mark==mark && auto.Model ==model && auto.Price == int.Parse(price))
                        findAutos.Add(auto);
                }
                else if(mark == "")
                {
                    if (model != "" && price != "")
                    {
                        if (auto.Model == model && auto.Price == int.Parse(price))
                            findAutos.Add(auto);
                    }
                    else if(model !=""&&price == "")
                    {
                        if (auto.Model == model )
                            findAutos.Add(auto);
                    }
                    else if(model == "" && price != "")
                    {
                        if (auto.Price == int.Parse(price))
                            findAutos.Add(auto);
                    }
                   
                }
                else if (model== "")
                {
                    if (mark != "" && price != "")
                    {
                        if (auto.Mark == mark && auto.Price == int.Parse(price))
                            findAutos.Add(auto);
                    }
                    else if (mark != "" && price == "")
                    {
                        if (auto.Mark == mark)
                            findAutos.Add(auto);
                    }
                    else if (mark == "" && price != "")
                    {
                        if (auto.Price == int.Parse(price))
                            findAutos.Add(auto);
                    }

                }
                else if (price == "")
                {
                    if (mark != "" && model != "")
                    {
                        if (auto.Mark == mark && auto.Model == model)
                            findAutos.Add(auto);
                    }
                    else if (mark != "" && model == "")
                    {
                        if (auto.Mark == mark)
                            findAutos.Add(auto);
                    }
                    else if (mark == "" && model != "")
                    {
                        if (auto.Model == model)
                            findAutos.Add(auto);
                    }

                }
            }

            return findAutos;
        }
    }
}

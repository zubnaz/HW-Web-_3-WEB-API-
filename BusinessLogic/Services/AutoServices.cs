using AutoMapper;
using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Dtos;
using BusinessLogic.Exceptions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataProject;
using DataProject.Data;
using DataProject.Data.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AutoServices : IAutosServices
    {
        public AutoServices(AutoDbContext adc, IMapper im)
        {
            Adc = adc;
            Im = im;
        }

        public AutoDbContext Adc { get; }
        private readonly IMapper Im;

       
        
        public async Task Create(CreateAutoModel auto)
        {
            Adc.Autos.Add(Im.Map<Auto>(auto));
            await Adc.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var auto = Adc.Autos.Find(id);
            if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
            Adc.Autos.Remove(auto);
            await Adc.SaveChangesAsync();
        }

        public async Task Edit(EditAutoModel auto)
        {
            Adc.Autos.Update(Im.Map<Auto>(auto));
            await Adc.SaveChangesAsync();
        }

        public Task<List<AutoDtos>> GetAsync()
        {
            return Task.Run(() =>
            {
                var list = Adc.Autos.Include(a => a.Color).ToList();
                if (list == null) throw new HttpException("List is empty!",HttpStatusCode.NotFound);
                return Im.Map<List<AutoDtos>>(list);
            });
        }

        public AutoDtos? Get(int id)
        {
            var auto = Adc.Autos.Include(a => a.Color).ToList().Find(a => a.Id == id);
            if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
            return Im.Map<AutoDtos>(auto);
        }

        public List<AutoDtos> Get()
        {
            var list = Adc.Autos.Include(a => a.Color).ToList();
            if (list == null) throw new HttpException("List is empty!", HttpStatusCode.NotFound);
            return Im.Map<List<AutoDtos>>(list);
        }

        public Task<AutoDtos>? GetAsync(int id)
        {
            return Task.Run(() =>
            {
                var auto = Adc.Autos.Include(a => a.Color).ToList().Find(a => a.Id == id);
                if (auto == null) throw new HttpException($"Auto with ID <{id}> not found!", HttpStatusCode.NotFound);
                return Im.Map<AutoDtos>(auto);
            });
        }
    }
}

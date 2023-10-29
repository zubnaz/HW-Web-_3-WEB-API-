using AutoMapper;
using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Dtos;
using DataProject.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class MapperConfigs:Profile
    {
        public MapperConfigs()
        {
            CreateMap<CreateAutoModel, Auto>().ReverseMap();
            CreateMap<EditAutoModel, Auto>().ReverseMap();
            CreateMap<AutoDtos,Auto>().ReverseMap();
        }
    }
}

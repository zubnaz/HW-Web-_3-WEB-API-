using AutoMapper;
using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Data.Entitys;
using BusinessLogic.Dtos;

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

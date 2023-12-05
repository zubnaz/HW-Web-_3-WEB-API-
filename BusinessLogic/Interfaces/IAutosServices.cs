using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IAutosServices
    {
        Task Create(CreateAutoModel auto);
        Task Edit(EditAutoModel auto);
        Task Delete(int id);
        List<AutoDtos> Get();
        Task<List<AutoDtos>> GetAsync();
        AutoDtos? Get(int id);
        Task<AutoDtos>? GetAsync(int id);
        List<AutoDtos> Sort(string type,string by);
        
    }
}

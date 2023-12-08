using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Data.Entitys;
using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IAccountServices
    {
        Task Register(RegisterAccount ra = null, RegisterAccountByAdmin raba = null);
        Task<LoginResponse> Login(LoginAccount la);
        Task ChangePassword(ChangePassword cp);
        Task Exit();
        Task<User> getUser();
        string IsSignIn();
        Task<string> IsAdmin();
    }
}

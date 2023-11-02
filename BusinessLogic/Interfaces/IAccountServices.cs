using BusinessLogic.ApiModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAccountServices
    {
        Task Register(RegisterAccount ra);
        Task Login(LoginAccount la);
        Task ChangePassword(ChangePassword cp);
        Task Exit();
    }
}

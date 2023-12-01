using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_Web__3_WEB_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices iAS;
        //test

        public AccountController(IAccountServices iAS)
        {
            this.iAS = iAS;
        }

        [HttpPost("registered")]
        public async Task<IActionResult> Register(RegisterAccount ra) 
        {
            await iAS.Register(ra:ra);
            return Ok();
        }
        
        [HttpPost("registered-admin")]
        public async Task<IActionResult> Register(RegisterAccountByAdmin raba)
        {
            await iAS.Register(raba:raba);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAccount la)
        {
            var key = await iAS.Login(la);
            return Ok(key);
        }
        [Authorize]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword cp)
        {
            await iAS.ChangePassword(cp);
            return Ok();
        }
        [HttpPost("exit")]
        public async Task<IActionResult> ExitAsync()
        {
            await iAS.Exit();
            return Ok();
        }
    }
}

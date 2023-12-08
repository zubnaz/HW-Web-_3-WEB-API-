using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Data.Entitys;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HW_Web__3_WEB_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices iAS;

        public AccountController(IAccountServices iAS)
        {
            this.iAS = iAS;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccount ra) 
        {
            await iAS.Register(ra:ra);
            return Ok("Ok");
        }
        
        [HttpPost("register-admin")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountByAdmin raba)
        {
            await iAS.Register(raba:raba);
            return Ok("Ok");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginAccount la)
        {
            var key = await iAS.Login(la);
            return Ok(key);
        }
        [Authorize]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword cp)
        {
            await iAS.ChangePassword(cp);
            return Ok("Ok");
        }
        [HttpPost("exit")]
        public async Task<IActionResult> ExitAsync()
        {
            await iAS.Exit();
            return Ok();
        }
        [HttpGet("check-signed")]
        public IActionResult CheckSigned()
        {
 
            return Ok(JsonSerializer.Serialize(iAS.IsSignIn()));
        }
        [HttpGet("role-check")]
        public async Task<IActionResult> CheckRole()
        {

            return Ok(JsonSerializer.Serialize(await iAS.IsAdmin()));
        }
        [HttpPut("Buy")]
        public async Task<IActionResult> Buy(int id)//buy car
        {
            iAS.Buy(id);
            return Ok();
        }
        [HttpGet("user-autos")]
        public async Task<IActionResult> GetUsersAuto()
        {
            return Ok(JsonSerializer.Serialize(await iAS.GetUsersAuto()));
        }
    }
}

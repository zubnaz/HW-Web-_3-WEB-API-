using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HW_Web__3_WEB_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutosController : ControllerBase
    {
        public AutosController(IAutosServices ias)
        {
            iAS = ias;
        }

        private readonly IAutosServices iAS;



        [HttpGet("all")]

        public IActionResult Get()
        {
            return Ok(iAS.Get());
        }
        [HttpGet("all-async")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await iAS.GetAsync());
        }
        [HttpGet("id")]
        public IActionResult GetByIdFromRoute([FromQuery] int id)
        {
            return Ok(iAS.Get(id));
        }
        [HttpGet("id-async")]
        public async Task<IActionResult> GetByIdFromRouteAsync([FromQuery] int id)
        {
            return Ok(await iAS.GetAsync(id));
        }
        //[Authorize(Roles = "Admin,Moderator")]
        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody]CreateAutoModel auto)
        {
            //await iAS.Create(auto);
            return Ok(auto);
        }
        [HttpGet("sort")]
        public IActionResult Sort([FromQuery] string type,string by)
        {
            return Ok(iAS.Sort(type,by));
        }
        [HttpGet("find")]
        public IActionResult Find([FromQuery] string mark = "", string model = "",string price = "")
        {
            return Ok(iAS.Find(mark,model,price));
        }
        /*public async Task<IActionResult> Add([FromQuery] CreateAutoModel auto)
        {
            //await iAS.Create(auto);
            return Ok("Okay");
        }*/
        [Authorize(Roles = "Admin,Moderator")]
        [HttpPut("Edit")]
        public async Task<IActionResult> Update([FromBody] EditAutoModel auto)
        {
            await iAS.Edit(auto);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await iAS.Delete(id);
            return Ok();
        }
    }
}

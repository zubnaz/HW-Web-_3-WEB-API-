using BusinessLogic.ApiModels.Autos;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id}")]
        public IActionResult GetByIdFromRoute([FromRoute] int id)
        {
            return Ok(iAS.Get(id));
        }
        [HttpGet("{id}-async")]
        public async Task<IActionResult> GetByIdFromRouteAsync([FromRoute] int id)
        {

            return Ok(await iAS.GetAsync(id));
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Add(CreateAutoModel auto)
        {
            await iAS.Create(auto);
            return Ok();
        }
        [Authorize]
        [HttpPut("Edit")]
        public async Task<IActionResult> Update(EditAutoModel auto)
        {
            await iAS.Edit(auto);
            return Ok();
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await iAS.Delete(id);
            return Ok();
        }
    }
}

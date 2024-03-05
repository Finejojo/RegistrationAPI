using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Repository.Interface;

namespace UserAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalCallController : ControllerBase
    {
        private readonly IExternalCall _call;
        public ExternalCallController(IExternalCall call)
        {
            _call = call;
        }

        [HttpGet("Get-User-Details")]
        public async Task<IActionResult> GetData()
        {
            var data = await _call.Fetch();
            if (data.Success)
                return Ok(data);
            return BadRequest(data);
        }
    }
}

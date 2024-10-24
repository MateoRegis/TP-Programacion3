using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthController(ICustomAuthenticationService customAuthenticationService)
        {
            _customAuthenticationService = customAuthenticationService;
        }
        [HttpPost]
        public async Task<ActionResult> LoginAsync(AuthRequest request)
        {
            try
            {
                var token = await _customAuthenticationService.Authenticate(request);

                return Ok(token);
            }
            catch (NotAllowedException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }
    }
}

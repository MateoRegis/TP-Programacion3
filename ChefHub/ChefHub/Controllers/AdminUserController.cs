using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IUserService _userService;
        public AdminUserController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> CreateUserAsync(UserRequest request)
        {
            var id = await _userService.CreateUserAsync(request);
            return Ok($"Se creo correctamente el usuario con el  {id}");
        }

    }
}

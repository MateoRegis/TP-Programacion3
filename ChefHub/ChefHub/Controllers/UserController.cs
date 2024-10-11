using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response == null)
            {
                return NotFound(new { success = false, message = "No se encontró un usuario con ese id" });
            }
            return Ok(new { success = true, data = response });
        }
        [HttpPost("[action]")]
        public async Task<ActionResult<UserResponse>> Register(UserRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { success = false, message = "El email y contraseña son obligatorios" });
            }
            var response = await _userService.Register(request);
            if (response == null)
            {
                return BadRequest(new { success = false, message = "Ya existe un usuario con este correo" });
            }
            return Created("", new { success = true, data = response });
        }

    }
}

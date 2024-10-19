using System.Security.Claims;
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
        [HttpGet("{idUser}")]
        public async Task<ActionResult<UserResponse>> GetUserById([FromRoute]int idUser)
        {
            var response = await _userService.GetUserById(idUser);
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

        [HttpPut("[action]")]
        public async Task<ActionResult> ModifyUser(UserRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { success = false, message = "Usuario no autorizado" });
            }
            await _userService.ModifyUser(request, int.Parse(userIdClaim));
            return Ok(new { success = true, message = "Usuario modificado" });
        }

    }
}

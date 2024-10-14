using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Domain.Enum;
using Domain.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {   
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> AddToFavorites([FromBody] FavoriteRequest request)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = "Usuario no autorizado" });
                }
                await _favoriteService.AddToFavorites(int.Parse(userIdClaim), request);
                return Ok(new { success = true });
                
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> DeleteFavorite([FromRoute] int favoriteId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = "Usuario no autorizado" });
                }
                await _favoriteService.DeleteFavorite(int.Parse(userIdClaim), favoriteId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
            catch (NotAllowedException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }

        [HttpPut("{favoriteId}")]
        public async Task<IActionResult> ModifyFavorite([FromRoute] int favoriteId, [FromBody] FavoriteRequest request)
        {
            try {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = "Usuario no autorizado" });
                }
                await _favoriteService.ModifyFavorite(int.Parse(userIdClaim), favoriteId, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
            catch (NotAllowedException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetFavoriteRecipesByUserAndType([FromQuery] FavoriteType favoriteType)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { success = false, message = "Usuario no autorizado" });
            }
            var response = await _favoriteService.GetFavoritesByUserAndType(int.Parse(userIdClaim), favoriteType);
            return Ok(new { success = true, data = response });
        }
    }
}

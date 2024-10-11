using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> CreateRecipe(RecipeRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { success = false, message = "Usuario no autorizado" });
            }
            var response = await _recipeService.CreateRecipe(request, int.Parse(userIdClaim));
            return Ok(new { success = true, data = response });
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetRecipesByUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { success = false, message = "Usuario no autorizado" });
            }
            var response = await _recipeService.GetRecipesByUser(int.Parse(userIdClaim));
            return Ok(new { success = true, data = response });
        }

    }

}
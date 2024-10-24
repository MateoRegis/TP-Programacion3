using System.Net;
using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Enum;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> CreateRecipe(RecipeRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { success = false, message = "Usuario no autorizado" });
            }
            try
            {
                if (string.IsNullOrEmpty(request.Difficulty.ToString()))
                {
                    return BadRequest(new { Success = false, Message = "La dificultad es obligatoria." });
                }
                var response = await _recipeService.CreateRecipe(request, int.Parse(userIdClaim));
                return Created("", new { success = true, data = response });
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }

        [HttpPut("{recipeId}")]
        [Authorize]
        public async Task<ActionResult> ModifyRecipe([FromBody] RecipeRequest request, [FromRoute] int recipeId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = "Usuario no autorizado" });
                }
                if (request.Difficulty != null)
                {
                    var difficultyExist = Enum.IsDefined(typeof(Difficulty), request.Difficulty);
                    if (!difficultyExist)
                    {
                        return BadRequest(new { Success = false, Message = "Dificultad no encontrada." });
                    };
                }

                await _recipeService.ModifyRecipe(request, recipeId, int.Parse(userIdClaim));
                return NoContent();

            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }

        [HttpDelete("{recipeId}")]
        [Authorize]
        public async Task<ActionResult> DeleteRecipe([FromRoute] int recipeId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = "Usuario no autorizado" });
                }
                var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                await _recipeService.DeleteRecipe(recipeId, int.Parse(userIdClaim), Enum.Parse<Role>(userRoleClaim));
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

        [HttpGet("GetRecipesByUser/{idUser}")]
        public async Task<ActionResult> GetRecipesByUser([FromRoute] int idUser)
        {
            try
            {
                var response = await _recipeService.GetRecipesByUser(idUser);
                return Ok(new { success = true, data = response });
            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllRecipes()
        {
            var response = await _recipeService.GetAllRecipes();
            return Ok(new { success = true, data = response });
        }

        [HttpGet("GetRecipeById/{idRecipe}")]
        public async Task<ActionResult> GetRecipeById([FromRoute] int idRecipe)
        {

            try
            {
                var response = await _recipeService.GetRecipeById(idRecipe);
                return Ok(new { success = true, data = response });

            }
            catch (NotFoundException ex)
            {

                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });

            }
        }
    }
}
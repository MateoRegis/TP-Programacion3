using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousRecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public AnonymousRecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllRecipes()
        {
            var response = await _recipeService.GetAllRecipes();
            return Ok(new { success = true, data = response });
        }
        [HttpGet("{idRecipe}")]

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

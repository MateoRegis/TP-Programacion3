using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
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
        [HttpPost("[action]")]
        public async Task<ActionResult> GetAllRecipes()
        {
            var response = await _recipeService.GetAllRecipes();
            return Ok(new { success = true, data = response });
        }
    }
}
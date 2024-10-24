using Application.Interfaces;
using Application.Models.Request;
using Domain.Enum;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChefHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> CreateComment([FromBody] CommentRequest request)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized(new { success = false, message = " Usuario No Autorizado" });

                }
                var response = await _commentService.CreateComment(request, int.Parse(userIdClaim));
                return Ok(new { success = true, data = response });

            }
            catch (NotFoundException ex)
            {
                return StatusCode((int)ex.Code, new { Success = false, Message = ex.Msg });
            }
        }
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment([FromRoute] int commentId, [FromQuery] int recipeId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                await _commentService.DeleteComment(recipeId, commentId, int.Parse(userIdClaim), Enum.Parse<Role>(userRoleClaim));

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

        [HttpPut("{commentId}")]
        public async Task<ActionResult> ModifyComment([FromBody] CommentRequest request, [FromRoute] int commentId)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await _commentService.ModifyComment(request, commentId, int.Parse(userIdClaim));

                return Ok(new { success = true, message = "Comentario modificado" });

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
    }
}

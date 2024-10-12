using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentResponse> CreateComment(CommentRequest request, int userId);
        Task<List<CommentResponse>> GetCommentsByRecipe(int recipeId);
        Task DeleteComment(int recipeId, int commentId, int userId, Role role);
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByRecipe(int recipeId);
    }
}

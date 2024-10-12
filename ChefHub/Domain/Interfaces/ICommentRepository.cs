using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetCommentsByRecipe(int recipeId);
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecipeRepository
    {
        public Task<List<Recipe>> GetRecipesByUser(int userId);
    }
}

using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface IFavoriteService
    {
        Task AddToFavorites(int userId, FavoriteRequest favoriteRequest);  
        Task<List<RecipeResponse>> GetFavoritesByUserAndType(int userId, FavoriteType favoriteType);
    }
}

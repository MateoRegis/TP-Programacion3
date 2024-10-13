using Domain.Entities;

namespace Application.Models.Response
{
    public class FavoriteResponse
    {
        public int Id { get; set; }
        public RecipeResponse? RecipeResponse { get; set; }

    }
}

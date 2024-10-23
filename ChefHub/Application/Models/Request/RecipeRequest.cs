using Domain.Enum;

namespace Application.Models.Request
{
    public class RecipeRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Ingredients { get; set; }
        public string? Instructions { get; set; }
        public string? UrlImage { get; set; }
        public List<string>? Categories { get; set; }
        public int? PreparationTime { get; set; }
        public Difficulty? Difficulty { get; set; }
    }
}

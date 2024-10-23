namespace Application.Models.Request
{
    public class CommentRequest
    {
        public string? Text { get; set; }
        public int  Score { get; set; }
        public int  RecipeId { get; set; }
    }
}

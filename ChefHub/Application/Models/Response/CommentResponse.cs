

namespace Application.Models.Response
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int Score { get; set; }
        public UserResponse?  UserResponse { get; set; }

    }
}

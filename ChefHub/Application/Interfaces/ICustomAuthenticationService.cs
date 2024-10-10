using Application.Models.Request;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string> Authenticate(AuthRequest request);
    }
}

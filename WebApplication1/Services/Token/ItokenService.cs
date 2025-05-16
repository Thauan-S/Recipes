using WebApplication1.Dto;

namespace WebApplication1.Services.Token
{
    public interface ItokenService
    {
        string GenerateToken(LoginRequest loginRequest);
    }
}

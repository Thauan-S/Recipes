using Tropical.Domain.Security.Tokens;

namespace Tropical.Infrastructure.Security.Tokens.Refresh
{
    public class RefreshTokenGenerator:IRefreshTokenGenerator
    {
        public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}

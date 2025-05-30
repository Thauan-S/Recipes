using Tropical.Domain.Security.Tokens;
using Tropical.Infrastructure.Security.Tokens.Refresh;

namespace CommonTestUtilities.Tokens
{
    public static class RefreshTokenGeneratorBuilder
    {
        public static IRefreshTokenGenerator Build()=> new RefreshTokenGenerator();
    }
}

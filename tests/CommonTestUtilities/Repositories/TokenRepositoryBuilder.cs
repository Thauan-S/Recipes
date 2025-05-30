using Moq;
using Tropical.Domain.Repositories.RefreshToken;

namespace CommonTestUtilities.Repositories
{
    public  class TokenRepositoryBuilder
    {
        private readonly  Mock<ITokenRepository> _tokenRepositoryMock;

        public TokenRepositoryBuilder()
        {
            _tokenRepositoryMock = new Mock <ITokenRepository>();
        }
        public ITokenRepository Build()
        {
            return _tokenRepositoryMock.Object;
        }
      
    }
}

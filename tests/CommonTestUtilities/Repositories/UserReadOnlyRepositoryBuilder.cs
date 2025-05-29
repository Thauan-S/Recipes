using Moq;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories
{
    public  class UserReadOnlyRepositoryBuilder
    {
        private  readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepositoryMock;

        public UserReadOnlyRepositoryBuilder()
        {
            _userReadOnlyRepositoryMock = new Mock<IUserReadOnlyRepository>();
        }

        public void ExistActiveUserWithEmail(string email)
        {
            _userReadOnlyRepositoryMock.Setup(r => r.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
        }
        public void GetByEmail(User user)
        {
            _userReadOnlyRepositoryMock.Setup(r => r.GetByEmail(user.Email)).ReturnsAsync(user);
        }
        public  IUserReadOnlyRepository Build()
        {
            return _userReadOnlyRepositoryMock.Object;
        }
    }
}

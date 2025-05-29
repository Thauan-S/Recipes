using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using Tropical.Application.UseCases.Login.DoLogin;
using Tropical.Comunication.Requests;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

namespace UseCases.Test.Login.DoLoginUseCaseTest
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            ///TODO USAR O MOQ
            (var user, var password) = UserBuilder.Build();
            // indicando que o UserBuilder retorna 2 params
            //mas devo indicar no userbuilder o nome
            var useCase = CreateUseCase(user);
            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = user.Email,
                Password = password,
            });
            Assert.NotNull(result);
            Assert.NotNull(result.Tokens.AccesToken);
            Assert.NotEmpty(result.Name);
            Assert.Equal(user.Name, result.Name);
        }
        [Fact]
        public async Task Error_Invalid_User()
        {
            var request = RequestLoginJsonBuilder.Build();

            var useCase = CreateUseCase();
            var act = async () => { await useCase.Execute(request); };

            var exception = await Assert.ThrowsAsync<InvalidLoginException>(() => act());
            Assert.Equal(ResourceMessagesException.EMAIL_OR_OASSWORD_INVALID, exception.Message);
        }

        private static DoLoginUseCase CreateUseCase(Tropical.Domain.Entities.User? user = null)
        {
            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
             var passwordEncripter = PasswordEncrypterBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            if (user != null)
            {
                userReadOnlyRepositoryBuilder.GetByEmail(user);
            }
              return new DoLoginUseCase( passwordEncripter, userReadOnlyRepositoryBuilder.Build(), accessTokenGenerator);
        }
    }
}

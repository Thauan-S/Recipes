using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using Tropical.Application.UseCases.User.Register;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;
using Tropical.Infrastructure.Security.Tokens.Refresh;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        ///TODO USAR O MOQ
        [Fact(DisplayName = "Should retun true when the request is valid")]
        public async Task Success()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);
            //Act
            var result = await useCase.Execute(request);
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Tokens);
            Assert.Equal(request.Name, result.Name);

        }
        [Fact]
        // como já tem os testes do validator, eu só preciso testar um erro aqui
        // e verificar se a exception está sendo lancada
        public async Task Error_Email_AlreadyExusts()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            var act = async () => await useCase.Execute(request);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Single(exception.ErrorMessages);
            Assert.Equal(ResourceMessagesException.INVALID_MAIL, exception.ErrorMessages.FirstOrDefault());
        }
        private  static RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var passwordEncrypter = PasswordEncrypterBuilder.Build();

            var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
            var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
            var tokenRepository = new TokenRepositoryBuilder();
            
            if (string.IsNullOrEmpty(email) == false)
            {
                readOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);
            }
            return new RegisterUserUseCase(readOnlyRepositoryBuilder.Build(), writeOnlyRepository, unityOfWork, mapper, passwordEncrypter, accessTokenGenerator,refreshTokenGenerator,tokenRepository.Build());
        }

        [Fact] // como já tem os testes do validator, eu só preciso testar um erro aqui
        // e verificar se a exception está sendo lancada
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var useCase = CreateUseCase();

            var act = async () => await useCase.Execute(request);

            var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(act);
            Assert.Single(exception.ErrorMessages);
            Assert.Equal(ResourceMessagesException.NAME_EMPTY, exception.ErrorMessages.First());
        }

        [Fact(DisplayName = "Should retun false when the  request name is empty")]
        public void ShouldReturnErronWhenNameIsEmpty()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            var validator = new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            var errors = result.Errors.Select(e => e.ErrorMessage);
            //Assert

            Assert.False(result.IsValid);
            Assert.Single(ResourceMessagesException.NAME_EMPTY,errors);
        }
        [Fact(DisplayName = "Should retun false when the  request name is empty")]
        public void ShouldReturnErrorWhenEmailIsEmpty()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;
            var validator = new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            //Assert
            var errors = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Single(ResourceMessagesException.EMPTY_MAIL, errors);

        }
        [Fact(DisplayName = "Should retun false when the  request name is empty")]
        public void ShouldReturnErrorWhenEmailNotIsvalid()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "email.com";
            var validator = new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            //Assert

            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.False(result.IsValid);
            Assert.Single(errors, ResourceMessagesException.INVALID_MAIL);
        }
        [Fact(DisplayName = "Should retun false when the  password  is invalid")]
        public void ShouldReturnErrorWhenPasswordNotIsValid()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = "a";
            var validator = new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            //Assert


            var errors = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Single(errors, ResourceMessagesException.INVALID_PASSWORD);
        }
        [Theory(DisplayName = "Should retun false when the  request name is empty")]
        [InlineData(1)]//posso passar mais de 1 parâmetro ex: (1,true)
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]

        public void ShouldReturnErrorWhenPasswordsNotIsValid(int passwordLength)// posso passar quantos parametros quiser
        {
            //Arrange                                                
            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);
            //request.Password = "password.com";
            var validator = new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            //Assert
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.False(result.IsValid);
            Assert.Equal(ResourceMessagesException.INVALID_PASSWORD, errors.FirstOrDefault());
        }
    }
}

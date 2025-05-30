using CommonTestUtilities.Requests;
using Tropical.Application.UseCases.User.Register;
using Tropical.Exceptions;

namespace Validators.test.User.Register
{
    public class RegisterUserValidatorTest
    {
        
        [Fact(DisplayName="Should retun true when the request is valid")]
       public  void Success()
        {
            //Arrange  ///TODO USAR O MOQ
            var request = RequestRegisterUserJsonBuilder.Build();
            var validator =new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            //Assert

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Should retun false when the  request name is empty")]
        public void Error_When_Empty()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name=string.Empty;
            var validator = new RegisterUserValidator();
            
            //Act
            var result = validator.Validate(request);
            var errors = result.Errors.Select(e=>e.ErrorMessage).ToList();
            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(ResourceMessagesException.NAME_EMPTY, result.Errors.Select(e => e.ErrorMessage));
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
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.False(result.IsValid);
            Assert.Equal(errors.First(), ResourceMessagesException.EMPTY_MAIL);
            
           
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
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            //Assert

            Assert.False(result.IsValid);
            Assert.Contains(ResourceMessagesException.INVALID_MAIL, result.Errors.Select(e => e.ErrorMessage));
        }
        [Fact(DisplayName = "Should retun false when the  request name is empty")]
        public void ShouldReturnErrorWhenPasswordNotIsValid()
        {
            //Arrange 
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Password = "email.com";
            var validator = new RegisterUserValidator();

            //Act
            var result = validator.Validate(request);
            //Assert
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            //Assert.False(result.IsValid);
            //Assert.Contains(ResourceMessagesException.INVALID_PASSWORD,errors.First());
            
        }
        [Theory(DisplayName = "Should retun false when the  request name is empty")]
        [InlineData(0)]
        [InlineData(1)]//posso passar mais de 1 parâmetro ex: (1,true)
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ShouldReturnErrorWhenPasswordsNotIsValid(int passwordLength)// posso passar quantos parametros quiser
        {
            //Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            //Act
            var result = validator.Validate(request);
            //Assert

           // Assert.False(result.IsValid);
           // Assert.Contains(ResourceMessagesException.INVALID_PASSWORD, result.Errors.Select(e => e.ErrorMessage));

        }
    }
}

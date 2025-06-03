using AutoMapper;
using Tropical.Comunication.Requests;
using Tropical.Comunication.Responses;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.RefreshToken;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Cryptography;
using Tropical.Domain.Security.Tokens;
using Tropical.Domain.Services.ServiceBus;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;
using Tropical.Infrastructure.Data;
using Tropical.Infrastructure.Security.Tokens.Refresh;

namespace Tropical.Application.UseCases.User.Register
{
    public class RegisterUserUseCase:IRegisterUserUseCase
    {
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenRepository _tokenRepository;
        private readonly ISendEmailUserQueue _sendEmailUserQueue;
        public RegisterUserUseCase(
            IUserReadOnlyRepository readOnlyRepository,
            IUserWriteOnlyRepository writeOnlyRepository,
            IUnityOfWork unityOfWork,
             IMapper mapper,
             IPasswordEncripter passwordEncripter
,
             IAccessTokenGenerator accessTokenGenerator,
             IRefreshTokenGenerator refreshTokenGenerator,
             ITokenRepository tokenRepository,
             ISendEmailUserQueue sendEmailUserQueue)
        {
            _passwordEncripter = passwordEncripter;
            _readOnlyRepository = readOnlyRepository;
            _unityOfWork = unityOfWork;
            _writeOnlyRepository = writeOnlyRepository;
            _mapper = mapper;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenRepository = tokenRepository;
            _sendEmailUserQueue = sendEmailUserQueue;
        }

        public async Task<ResponseRegisterUserJson>  Execute(RequestRegisterUserJson request)
        {  
           
           await Validate(request);
            

            var user = _mapper.Map<Domain.Entities.User>(request);
            
            user.Password = _passwordEncripter.Encrypt(request.Password);
            

            await _writeOnlyRepository.AddUser(user);
            await _unityOfWork.Commit();

            await _sendEmailUserQueue.SendMessage(user.Email);

            var refreshToken = await CreateAndSaveRefreshToken(user);
            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Tokens = new ResponseTokensJson
                {
                    AccesToken = _accessTokenGenerator.Generate(user.UserId),
                    RefreshToken=refreshToken
                }

            };
        }

        private async  Task Validate(RequestRegisterUserJson user)
        {
            var validator = new RegisterUserValidator();//classe criada para validar os campos do user
            
            var result= validator.Validate(user);
            var emailExist=await _readOnlyRepository.ExistActiveUserWithEmail(user.Email);
            if (emailExist) {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,ResourceMessagesException.INVALID_MAIL));
            }

            if (result.IsValid==false)// isvalid retorna true ou false de acordo com o validator
            {
                var errorMessages=result.Errors.Select(e=>e.ErrorMessage).ToList();// usando linq pois o .Errors retorna uma lista de result validations
                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
        {
            var refreshToken = _refreshTokenGenerator.Generate();

            await _tokenRepository.SaveNewRefreshToken(new RefreshToken()
            {
                Value = refreshToken,
                UserId = user.Id
            });

            await _unityOfWork.Commit();

            return refreshToken;
        }


    }
}

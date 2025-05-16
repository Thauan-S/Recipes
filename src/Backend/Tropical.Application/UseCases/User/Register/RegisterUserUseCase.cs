using AutoMapper;
using Tropical.Comunication.Requests;
using Tropical.Domain.Repositories;
using Tropical.Domain.Repositories.User;
using Tropical.Domain.Security.Cryptography;
using Tropical.Domain.Security.Tokens;
using Tropical.Exceptions;
using Tropical.Exceptions.Exceptions;

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
        public RegisterUserUseCase(
            IUserReadOnlyRepository readOnlyRepository,
            IUserWriteOnlyRepository writeOnlyRepository,
            IUnityOfWork unityOfWork,
             IMapper mapper,
             IPasswordEncripter passwordEncripter
,
             IAccessTokenGenerator accessTokenGenerator)
        {
            _passwordEncripter = passwordEncripter;
            _readOnlyRepository = readOnlyRepository;
            _unityOfWork = unityOfWork;
            _writeOnlyRepository = writeOnlyRepository;
            _mapper = mapper;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisterUserJson>  Execute(RequestRegisterUserJson request)
        {  
           
           await Validate(request);
            //validar

            var user = _mapper.Map<Domain.Entities.User>(request);
            //mapear 
            user.Password = _passwordEncripter.Encrypt(request.Password);
            //criptografia da senha
            user.UserId=Guid.NewGuid(); // atribuindo o ID que será gerado no token 

            await _writeOnlyRepository.AddUser(user);
            await _unityOfWork.Commit();
            //salvar no db

            var token = _accessTokenGenerator.Generate(user.UserId);
            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Token = token

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

    }
}

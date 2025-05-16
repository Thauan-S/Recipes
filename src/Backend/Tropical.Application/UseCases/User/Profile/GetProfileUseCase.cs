
using AutoMapper;
using Tropical.Comunication.Responses;
using Tropical.Domain.Services.LoggedUser;

namespace Tropical.Application.UseCases.User.Profile
{
    internal class GetProfileUseCase : IGetUserProfileUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;
        public GetProfileUseCase(ILoggedUser loggedUser, IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }

        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggedUser.User();
            return _mapper.Map<ResponseUserProfileJson>(user);
            
          
        }
    }
}

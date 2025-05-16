using Tropical.Domain.Entities;

namespace Tropical.Domain.Services.LoggedUser
{
    public interface ILoggedUser // interface que identifica o usuário logado
    {
        public Task<User> User();
    }
}
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IClientRepository
    {
      
        Task<ResponseModel<ClientDto>>? FindByEmail(string email);
        Task<ResponseModel<ClientDto>> Create(ClientDto clientDto);
    }
}

using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Services.Client
{
    public interface IClientService
    {
        //task significa métodos assíncronos
        //Task<ResponseModel<List<ClientModel>>> FindAllClients();
        //Task<ResponseModel<ClientModel>> FindById(Guid id);
        Task<ResponseModel<ClientDto>>? FindByEmail(string email);
        //Task<ResponseModel<ClientModel>> DeleteById(Guid id);
        Task<ResponseModel<ClientDto>> Create(ClientDto clientDto);
        //Task<ResponseModel<ClientDto>> Update(ClientDto clientDto);
    }
}

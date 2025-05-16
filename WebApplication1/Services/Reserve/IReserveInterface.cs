using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Services.Reserve
{
    public interface IReserveInterface
    {
        //task significa métodos assíncronos
        Task<ResponseModel<List<ReserveDto>>> FindAllReserves();
        Task<ResponseModel<ReserveDto>> FindById(int id);
        Task<ResponseModel<ReserveModel>> DeleteById(int id);
        Task<ResponseModel<ReserveDto>> Create(ReserveDto reserveDto);
        Task<ResponseModel<ReserveDto>> Update(ReserveDto reserveDto);
    }
}

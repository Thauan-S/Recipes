//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Context;
//using WebApplication1.Dto;
//using WebApplication1.Models;
//using WebApplication1.Services.Client;

//namespace WebApplication1.Services.Reserve
//{
//    public class ReserveService : IReserveInterface
//    {
//        // context
//        private readonly AppDbContext _context;
//        //construtor
//        public ReserveService(AppDbContext context) => _context = context;

//        public async Task<ResponseModel<ReserveDto>> Create(ReserveDto reserveDto)
//        {
//            ResponseModel<ReserveDto> response = new ResponseModel<ReserveDto>();
//            try
//            {
//                var reserve = new ReserveModel() // não precisei criar o construtor na classe model;
//                {
//                    CreationDate = DateTime.Now,
//                    TravelPackage = reserveDto.TravelPackage,
//                    TravelDate = reserveDto.TravelDate,
//                    Client = reserveDto.Client,
//                };
//                _context.Reserves.Add(reserve);
//                await _context.SaveChangesAsync();
               
//                response.Data = reserveDto;
//                response.Status=true;
//                response.Message = "Cliente foi criado com sucesso";
//                return response;
//            }
//            catch (Exception ex)
//            {
//                response.Status=false;
//                response.Message = ex.Message;
//                return response;
//            }
//        }

        
//        public async Task<ResponseModel<ReserveModel>> DeleteById(int id)
//        {
//            ResponseModel<ReserveModel> response =new ResponseModel<ReserveModel>() ;
//            try
//            {
//                var reserve = await _context.Reserves.FirstOrDefaultAsync(r=> r.Id == id);
//                if (reserve == null)
//                {
//                    response.Status = false;
//                    response.Message = "Reserva de id" + id + " não encontrado na base de dados";
//                    return response;

//                }
//                  _context.Reserves.Remove(reserve);
//                await _context.SaveChangesAsync();
//                response.Data = reserve;
//                response.Status = true;
//                response.Message = "Reserva removida do banco de dados";

//                return response;
//            }
//            catch (Exception ex) { 
//                response.Message = ex.Message;
//                return response;
//            }
            
//        }
//        public async Task<ResponseModel<List<ReserveDto>>> FindAllReserves()
//        {
//            ResponseModel<List<ReserveDto>> response = new ResponseModel<List<ReserveDto>>();
//            try
//            {
//                var reserves = await _context.Reserves.ToListAsync();
                
//                var reservesDto=reserves.Select(r => new ReserveDto()
//                {
//                    Id = r.Id,
//                    TravelDate = r.TravelDate,
//                    Client=r.Client,
//                    TravelPackage=r.TravelPackage,
//                }).ToList();
               
//                response.Data = reservesDto;
//                response.Message = "Todas as reservas foram coletadas";
//                response.Status = true;
//                return response;
//            }
//            catch (Exception e)
//            {
//                response.Message = e.Message;
//                response.Status = false;
//                return response;
//            }
//        }

//        public async Task<ResponseModel<ReserveDto>> FindById(int id)
//        {
//            ResponseModel<ReserveDto> response = new ResponseModel<ReserveDto>();
            
//            try
//            {
//                var reserve = await _context.Reserves.FirstOrDefaultAsync(r => r.Id == id);
//                if (reserve == null)
//                {
//                    response.Message = "A reserva de id " + id + "não foi encontrado na base de dados";
//                    return response;
//                }
//                var reserveDto = new ReserveDto() // converte de entity para dto
//                {
//                    CreationDate = DateTime.Now,
//                    TravelPackage = reserve.TravelPackage,
//                    TravelDate = reserve.TravelDate,
//                    Client = reserve.Client
//                };
//                response.Data = reserveDto;
//                response.Message = "reserva localizada";
//                response.Status = true;
//                return response;

//            }
//            catch (Exception ex)
//            {
//                response.Message += ex.Message;
//                response.Status = false;
//                return response;
//            }
//        }

//        public async Task<ResponseModel<ReserveDto>> Update(ReserveDto reserveDto)
//        {
//            ResponseModel<ReserveDto> response = new ResponseModel<ReserveDto>();
//            try {
                
//                var reserve = await _context.Reserves
//                    .FirstOrDefaultAsync(r => r.Id == reserveDto.Id);
//                if (reserve == null)
//                {
//                    response.Status= false;
//                    response.Message = "Reserva não encontrado na base de dados";
//                }
                
//                reserve.TravelPackage = reserveDto.TravelPackage;
//                reserve.CreationDate = DateTime.Now;
//                reserve.TravelDate= reserveDto.TravelDate;
//                _context.Reserves.Update(reserve);
//                await _context.SaveChangesAsync();
//                response.Status = true;
//                response.Data = reserveDto;
//                return response;
                
//            }
//            catch (Exception ex) { 
//             response.Message = ex.Message;
//                response.Status= false;
//                return response;
//            }

           
            

//        }

//    }
//}  


            
               
        


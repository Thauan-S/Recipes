//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Context;
//using WebApplication1.Dto;
//using WebApplication1.Models;

//namespace WebApplication1.Repositories
//{
//    public class ClientRepository:IClientRepository
//    {
//        private readonly AppDbContext _context;

//        public ClientRepository(AppDbContext context) {
//        _context = context;
//        }


    
//        public async  Task<ResponseModel<ClientDto>> Create(ClientDto clientDto)
//        {
//            ResponseModel<ClientDto> response = new ResponseModel<ClientDto>();
//            try
//            {
//                var clientFromDb = await _context.Clients.FirstOrDefaultAsync(c => c.Email == clientDto.Email);
//                if (clientFromDb != null) throw new Exception("Já existe um cliente cadastrado com esse email");
//                var client = new ClientModel() // não precisei criar o construtor na classe model;
//                {
//                    Name = clientDto.Name,
//                    Email = clientDto.Email,
//                    Password = clientDto.Password,
//                    Phone = clientDto.Phone,
//                    Role = "basic",

//                };
//                _context.Clients.Add(client);
//                await _context.SaveChangesAsync();

//                response.Data = clientDto;
//                response.Status = true;
//                response.Message = "Cliente foi criado com sucesso";
//                return response;
//            }
//            catch (Exception ex)
//            {
//                response.Status = false;
//                response.Message = ex.Message;
//                return response;
//            }
//        }

//        public async  Task<ResponseModel<ClientDto>>? FindByEmail(string email)
//        {
//            ResponseModel<ClientDto> response = new ResponseModel<ClientDto>();
//            try
//            {
//                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
//                if (client == null)
//                {
//                    response.Status = false;
//                    response.Message = "Cliente de email:" + email + " não encontrado na base de dados";
//                    return response;
//                }

//                var clientDto = new ClientDto()
//                {
//                    Email = client.Email,
//                    Name = client.Name,
//                    Password = client.Password,
//                    Phone = client.Phone,
//                    Role = client.Role,
//                };
//                response.Data = clientDto;
//                response.Status = true;
//                response.Message = "Cliente removido do banco de dados";

//                return response;
//            }
//            catch (Exception ex)
//            {
//                response.Message = ex.Message;
//                return response;
//            }
//        }

       
//    }
//}

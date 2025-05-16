//using WebApplication1.Dto;
//using WebApplication1.Models;
//using WebApplication1.Repositories;


//namespace WebApplication1.Services.Client
//{
//    public class ClientService : IClientService
//    {


//        private readonly IClientRepository _clientRepository;


//        //construtor
//        public ClientService(IClientRepository clientRepository )
//        {
//            _clientRepository = clientRepository;
//        }

//        public Task<ResponseModel<ClientDto>> Create(ClientDto clientDto)
//        {
//            //ResponseModel<ClientDto> response = new ResponseModel<ClientDto>();
//            //try
//            //{
//            //    var clientFromDb=await _context.Clients.FirstOrDefaultAsync(c=> c.Email==clientDto.Email);
//            //    if (clientFromDb != null) throw new Exception("Já existe um cliente cadastrado com esse email");
//            //    var client = new ClientModel() // não precisei criar o construtor na classe model;
//            //    {
//            //        Name = clientDto.Name,
//            //        Email = clientDto.Email,
//            //        Password = clientDto.Password,
//            //        Phone = clientDto.Phone,
//            //        Role = Roles.BASIC.ToString(),

//            //    };
//            //    _context.Clients.Add(client);
//            //    await _context.SaveChangesAsync();

//            //    response.Data = clientDto;
//            //    response.Status=true;
//            //    response.Message = "Cliente foi criado com sucesso";
//            //    return response;
//            //}
//            //catch (Exception ex)
//            //{
//            //    response.Status=false;
//            //    response.Message = ex.Message;
//            //    return response;
//            //}
//            return _clientRepository.Create(clientDto);
//        }

//        //public async Task<ResponseModel<ClientModel>> DeleteById(Guid id)
//        //{
//        //    ResponseModel<ClientModel> response =new ResponseModel<ClientModel>() ;
//        //    try
//        //    {
//        //        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
//        //        if (client == null)
//        //        {
//        //            response.Status = false;
//        //            response.Message = "Cliente de id" + id + " não encontrado na base de dados";
//        //            return response;

//        //        }
//        //          _context.Clients.Remove(client);
//        //        await _context.SaveChangesAsync();
//        //        response.Data = client;
//        //        response.Status = true;
//        //        response.Message = "Cliente removido do banco de dados";

//        //        return response;
//        //    }
//        //    catch (Exception ex) { 
//        //        response.Message = ex.Message;
//        //        return response;
//        //    }

//        //}

//        //public async Task<ResponseModel<List<ClientModel>>> FindAllClients()
//        //{
//        //    ResponseModel<List<ClientModel>> response = new ResponseModel<List<ClientModel>>();
//        //    try
//        //    {
//        //        var clients = await _context.Clients.ToListAsync();

//        //        response.Data = clients;
//        //        response.Message = "Todos os clientes foram coletados";
//        //        response.Status = true;
//        //        return response;
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        response.Message = e.Message;
//        //        response.Status = false;
//        //        return response;
//        //    }
//        //}

//        public Task<ResponseModel<ClientDto>>? FindByEmail(string email)
//        {
//            return _clientRepository.FindByEmail(email);
//        }

//        //public async Task<ResponseModel<ClientModel>> FindById(Guid id)
//        //{
//        //    ResponseModel<ClientModel> response = new ResponseModel<ClientModel>();
//        //    try
//        //    {
//        //        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
//        //        if (client == null)
//        //        {
//        //            response.Message = "O cliente de id " + id + "não foi encontrado na base de dados";
//        //            return response;
//        //        }
//        //        response.Data = client;
//        //        response.Message = "cliente localizado";
//        //        response.Status = true;
//        //        return response;

//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        response.Message += ex.Message;
//        //        response.Status = false;
//        //        return response;
//        //    }
//        //}

//        //public async Task<ResponseModel<ClientDto>> Update(ClientDto clientDto)
//        //{
//        //    ResponseModel<ClientDto> response = new ResponseModel<ClientDto>();
//        //    try {

//        //        var client = await _context.Clients
//        //            .FirstOrDefaultAsync(c => c.Id == clientDto.Id);
//        //        if (client == null)
//        //        {
//        //            response.Status= false;
//        //            response.Message = "Cliente não encontrado na base de dados";
//        //        }

//        //        client.Name=clientDto.Name;
//        //        client.Phone = clientDto.Phone;
//        //        client.Email=clientDto.Email;
//        //        client.Password=clientDto.Password;
//        //        _context.Clients.Update(client);
//        //        await _context.SaveChangesAsync();
//        //        response.Status = true;
//        //        response.Data = clientDto;
//        //        return response;

//        //    }
//        //    catch (Exception ex) { 
//        //     response.Message = ex.Message;
//        //        response.Status= false;
//        //        return response;
//        //    }




//        //}
//    }
//}







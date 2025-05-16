namespace WebApplication1.Models
{
    // o tipo de response é generico , pois posso ter clientes, reservas, pacotes de viagem etc
    public class ResponseModel <T>
    {
        public T? Data  { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}

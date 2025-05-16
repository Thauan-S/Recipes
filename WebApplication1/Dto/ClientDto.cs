using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Dto
{
    public class ClientDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public required  string  Name  { get; set; }
       
        public required string Phone { get; set; }
        
        public required string Email { get; set; }
        public required string  Password { get; set; }
        [JsonIgnore]
        public string? Role { get; set; }
        
    }
}

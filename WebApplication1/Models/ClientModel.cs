using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace WebApplication1.Models
{
    [Table("clients")]
    public class ClientModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(15)]
        public required string  Phone { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [JsonIgnore]// não é necessário um cliente ter as reservas na sua criação
        public ICollection<ReserveModel>?  Reserves { get; set; }
        [JsonIgnore]
        public  string? Role {  get; set; }

    }
    enum Roles
    {
        BASIC,
        ADMIN,
    }
}

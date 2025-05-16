using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("reserves")]
    public class ReserveModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime TravelDate { get; set; }
        public ClientModel Client { get; set; }  
        public TravelPackage TravelPackage { get; set; }    

    }
}

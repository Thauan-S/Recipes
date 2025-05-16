using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    [Table("travel_packages")]
    public class TravelPackage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Destiny { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        public int Days { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Category { get; set; }
        [JsonIgnore]
        public ICollection<ReserveModel> Reserves { get; set; }
    }
}

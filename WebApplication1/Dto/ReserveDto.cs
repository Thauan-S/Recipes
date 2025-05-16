using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;

namespace WebApplication1.Dto
{
    
    public class ReserveDto
    {
        
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime TravelDate { get; set; }
        public ClientModel Client { get; set; }  
        public TravelPackage TravelPackage { get; set; }    

    }
}

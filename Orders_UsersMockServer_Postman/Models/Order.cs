using System.ComponentModel.DataAnnotations;

namespace Orders_UsersMockServer_Postman.Models
{
    public class Order
    {
         public int Id { get; set; } // Auto-incremented

        [Required]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }
        
    }
}

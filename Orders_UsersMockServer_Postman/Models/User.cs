using System.ComponentModel.DataAnnotations;

namespace Orders_UsersMockServer_Postman.Models
{
    public class User
    {
        [Required]
        public int userId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}

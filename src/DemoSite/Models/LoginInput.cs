using System.ComponentModel.DataAnnotations;

namespace DemoSite.Models
{
    public class LoginInput
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dto
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = "amro@gmail.com";
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "123123";
    }
}


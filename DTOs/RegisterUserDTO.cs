using System.ComponentModel.DataAnnotations;

namespace QuizWorkShop.DTOs
{
    public class RegisterUserDTO
    {
            [Required]
            public string userName { get; set; }
            [Required]
            public string password { get; set; }
            public string ?email { get; set; }
            public string? phoneNumber { get; set; }
    }
}

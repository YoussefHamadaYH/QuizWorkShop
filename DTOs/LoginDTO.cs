using System.ComponentModel.DataAnnotations;

namespace QuizWorkShop.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

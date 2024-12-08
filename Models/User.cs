using System.ComponentModel.DataAnnotations;

namespace QuizWorkShop.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public virtual ICollection<Quiz>? Quizzes { get; set; } = new List<Quiz>();
        public virtual ICollection<Answer>? Answers { get; set; } = new List<Answer>();
    }
}

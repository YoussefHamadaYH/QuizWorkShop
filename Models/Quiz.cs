using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWorkShop.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public string QuizDescription { get; set; }
        public string? ImageUrl {  get; set; }
        public DateTime Date {  get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User ? User { get; set; }
        public virtual ICollection<Question>? Questions { get; set; } = new List<Question>();
        public virtual ICollection<Answer>? Answers { get; set; } = new List<Answer>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWorkShop.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public virtual Quiz? Quiz { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
}

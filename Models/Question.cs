using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWorkShop.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerType { get; set; }
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public virtual Quiz? Quiz { get; set; }
        public virtual ICollection<Choices>? Choices { get; set; } = new List<Choices>();
        public virtual ICollection<Answer>? Answers { get; set; } = new List<Answer>();
    }
}

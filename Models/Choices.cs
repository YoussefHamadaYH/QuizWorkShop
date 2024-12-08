using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizWorkShop.Models
{
    public class Choices
    {
        [Key]
        public int ChoiceId { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
}

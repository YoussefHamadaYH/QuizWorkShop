using QuizWorkShop.Models;

namespace QuizWorkShop.DTOs
{
    public class AnswerDTO
    {
        public string AnswerText { get; set; }
        public int QuizId { get; set; }      // Foreign key for Quiz
        public int QuestionId { get; set; }  // Foreign key for Question
        public int UserId { get; set; }      // Foreign key for User
        public int Id { get; set; }
    }

}

using QuizWorkShop.Models;

namespace QuizWorkShop.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public string QuizDescription { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public List<QuestionDTO>? Questions { get; set; } = new List<QuestionDTO>();
        public List<AnswerDTO>? Answers { get; set; } = new List<AnswerDTO>();
    }

}

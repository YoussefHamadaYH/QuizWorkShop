namespace QuizWorkShop.DTOs
{
    public class QuestionDTO
    {
        public string QuestionText { get; set; }
        public string AnswerType { get; set; }
        public int QuizId { get; set; }
        public int Id { get; set; }
        public List<ChoiceDTO>? Choices { get; set; } = new List<ChoiceDTO>();
        public List<AnswerDTO>? Answers { get; set; } = new List<AnswerDTO>();
    }
}

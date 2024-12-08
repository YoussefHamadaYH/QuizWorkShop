namespace QuizWorkShop.DTOs
{
    public class ChoiceDTO
    {
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public int Id { get; set; }
        public string ChoiceText { get; set; }
    }
}

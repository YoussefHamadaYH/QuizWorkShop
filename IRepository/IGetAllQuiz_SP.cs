using QuizWorkShop.Models;

namespace QuizWorkShop.IRepository
{
    public interface IGetAllQuiz_SP
    {
        // Add a method to get all quizzes from the stored procedure
        IEnumerable<Quiz> GetAllQuizzesFromSP();
    }
}

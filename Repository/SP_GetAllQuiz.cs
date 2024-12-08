using Microsoft.EntityFrameworkCore;
using QuizWorkShop.IRepository;
using QuizWorkShop.Models;

namespace QuizWorkShop.Repository
{
    public class SP_GetAllQuiz : IGetAllQuiz_SP
    {
        private readonly QuizContext context;
        public SP_GetAllQuiz(QuizContext _context)
        {
            context = _context;
        }
        public IEnumerable<Quiz> GetAllQuizzesFromSP()
        {
            // Execute the stored procedure to get all quizzes
            var quizzes = context.Quizzes.FromSqlRaw("EXEC GetAllQuizzesWithHisQuestions").ToList();
            return quizzes;
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuizWorkShop.Models
{
    public class QuizContext : IdentityDbContext<ApplicationUser>
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Choices> Choices { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "Admin", password = "admin123" },
                new User { UserId = 2, UserName = "User1", password = "user123" }
            );

            // Seeding Quizzes
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    QuizId = 1,
                    QuizName = "General Knowledge Quiz",
                    QuizDescription = "Test your general knowledge",
                    ImageUrl = "https://example.com/quiz1.png",
                    Date = DateTime.Now,
                    UserId = 1
                }
            );

            // Seeding Questions
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    QuestionId = 1,
                    QuestionText = "What is the capital of France?",
                    AnswerType = "Multiple Choice",
                    QuizId = 1
                }
            );

            // Seeding Choices
            modelBuilder.Entity<Choices>().HasData(
                new Choices { ChoiceId = 1, OptionText = "Paris", IsCorrect = true, QuestionId = 1 },
                new Choices { ChoiceId = 2, OptionText = "Berlin", IsCorrect = false, QuestionId = 1 },
                new Choices { ChoiceId = 3, OptionText = "Madrid", IsCorrect = false, QuestionId = 1 }
            );

            // Seeding Answers
            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    AnswerId = 1,
                    AnswerText = "Paris",
                    UserId = 2,
                    QuizId = 1,
                    QuestionId = 1
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}

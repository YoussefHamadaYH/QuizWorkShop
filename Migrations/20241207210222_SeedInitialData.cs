using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizWorkShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserName", "password" },
                values: new object[,]
                {
                    { 1, "Admin", "admin123" },
                    { 2, "User1", "user123" }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "QuizId", "Date", "ImageUrl", "QuizDescription", "QuizName", "UserId" },
                values: new object[] { 1, new DateTime(2024, 12, 7, 23, 2, 20, 404, DateTimeKind.Local).AddTicks(6627), "https://example.com/quiz1.png", "Test your general knowledge", "General Knowledge Quiz", 1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "AnswerType", "QuestionText", "QuizId" },
                values: new object[] { 1, "Multiple Choice", "What is the capital of France?", 1 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "AnswerId", "AnswerText", "QuestionId", "QuizId", "UserId" },
                values: new object[] { 1, "Paris", 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Choices",
                columns: new[] { "ChoiceId", "IsCorrect", "OptionText", "QuestionId" },
                values: new object[,]
                {
                    { 1, true, "Paris", 1 },
                    { 2, false, "Berlin", 1 },
                    { 3, false, "Madrid", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "AnswerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Choices",
                keyColumn: "ChoiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Choices",
                keyColumn: "ChoiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Choices",
                keyColumn: "ChoiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "QuizId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}

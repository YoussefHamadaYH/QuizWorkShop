using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWorkShop.DTOs;
using QuizWorkShop.IRepository;
using QuizWorkShop.Models;
using QuizWorkShop.Repository;

namespace QuizWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<Question> QuestionRepository;
        private readonly IGetAllQuiz_SP sP_GetAllQuiz;
        private readonly IMapper _mapper;

        public QuizController(IGenericRepository<Quiz> quizRepository, IMapper mapper, IGetAllQuiz_SP sP_GetAllQuiz , IGenericRepository<Question> QuestionRepository)
        {
            this.quizRepository = quizRepository;
            this._mapper = mapper;
            this.sP_GetAllQuiz = sP_GetAllQuiz; 
            this.QuestionRepository = QuestionRepository;
        }

        [HttpGet]
        public IActionResult GetAllQuizzes() 
        {
            //Apply Stored Procedure
            var quizzes = sP_GetAllQuiz.GetAllQuizzesFromSP();
            return Ok(quizzes);
        }
        [HttpGet("{id}")]
        public IActionResult GetQuizById(int id) 
        { 
            var quiz = quizRepository.GetById(id);
            if (quiz == null) 
                return NotFound("Quiz not found.");
            var quizMapp = _mapper.Map<QuizDTO>(quiz);
            return Ok(quizMapp);
        }
        [HttpGet("FilterByName")]
        public IActionResult FilterQuizzesByName(string name)
        {
            var quizzes = quizRepository.GetAll()
                .Where(q => q.QuizName== name)
                .Select(q => _mapper.Map<QuizDTO>(q))
                .ToList();
            if (quizzes==null || !quizzes.Any()) 
            { return NotFound("No quizzes found matching the given name."); }
            return Ok(quizzes);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult AddQuiz(QuizDTO quizDTO)
        {
            if (quizDTO == null || !ModelState.IsValid)
                return BadRequest();

            // Check if the UserId exists
            var userExists = quizRepository.GetAll().FirstOrDefault(u => u.UserId == quizDTO.UserId);
            if (userExists == null)
                return BadRequest("Invalid UserId. The specified user does not exist.");

            // Map DTO to the entity model
            var quiz = _mapper.Map<Quiz>(quizDTO);
            quizRepository.Create(quiz);
            quizRepository.Save();
            return CreatedAtAction("GetQuizById", new { id = quiz.QuizId }, quiz);
        } 
        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult UpdateQuiz(QuizDTO quizDTO, int id)
        {
            if (quizDTO == null)
                return BadRequest("Quiz data is required.");

            // Check if the quiz exists
            var existingQuiz = quizRepository.GetById(id);
            if (existingQuiz == null)
                return NotFound("Quiz not found.");

            // Validate UserId
            var userExists = quizRepository.GetAll().FirstOrDefault(u => u.UserId == quizDTO.UserId);
            if (userExists == null)
                return BadRequest("Invalid UserId. The specified user does not exist.");

            // Map DTO to the existing entity
            existingQuiz.QuizName = quizDTO.QuizName;
            existingQuiz.QuizDescription = quizDTO.QuizDescription;
            existingQuiz.ImageUrl = quizDTO.ImageUrl;
            existingQuiz.Date = quizDTO.Date;
            existingQuiz.UserId = quizDTO.UserId; // Ensure UserId is valid

            quizRepository.Update(existingQuiz);
            quizRepository.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteQuiz(int id) 
        {
            quizRepository.Delete(id);
            quizRepository.Save();
            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWorkShop.DTOs;
using QuizWorkShop.IRepository;
using QuizWorkShop.Models;

namespace QuizWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IGenericRepository<Answer> _answerRepository;
        private readonly IMapper _mapper;
        public AnswerController (IGenericRepository<Answer> answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllAnswers()
        {
            var Answers = _answerRepository.GetAll();
            return Ok(Answers);
        }
        [HttpGet("{id}")]
        public IActionResult GetAnswerById(int id)
        {
            var Answer = _answerRepository.GetById(id);
            if (Answer == null)
                return NotFound();
            return Ok(Answer);
        }
        [HttpPost]
        public IActionResult AddAnswer(AnswerDTO answerDTO)
        {
            if (answerDTO == null || !ModelState.IsValid)
                return BadRequest("Invalid data.");

            // Validate if the QuizId, QuestionId, and UserId exist in the database
            var quizExists = _answerRepository.GetAll().FirstOrDefault(q => q.QuizId == answerDTO.QuizId);
            var questionExists = _answerRepository.GetAll().FirstOrDefault(q => q.QuestionId == answerDTO.QuestionId);
            var userExists = _answerRepository.GetAll().FirstOrDefault(q => q.UserId == answerDTO.UserId);

            if (quizExists == null || questionExists == null || userExists == null)
                return BadRequest("Invalid QuizId, QuestionId, or UserId. One or more provided references do not exist.");

            // Map DTO to the entity model
            var answer = _mapper.Map<Answer>(answerDTO);

            // Create the answer and save it to the repository
            _answerRepository.Create(answer);
            _answerRepository.Save();

            // Return the created answer as a DTO
            return CreatedAtAction("GetAnswerById", new { id = answer.AnswerId }, answer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnswer(int id, AnswerDTO answerDTO)
        {
            if (!ModelState.IsValid || answerDTO == null)
                return BadRequest("Invalid data.");

            var existingAnswer = _answerRepository.GetById(id);
            if (existingAnswer == null)
                return NotFound("Answer not found.");

            // Validate if the QuizId, QuestionId, and UserId exist in the database
            var quizExists = _answerRepository.GetAll().FirstOrDefault(q => q.QuizId == answerDTO.QuizId);
            var questionExists = _answerRepository.GetAll().FirstOrDefault(q => q.QuestionId == answerDTO.QuestionId);
            var userExists = _answerRepository.GetAll().FirstOrDefault(q => q.UserId == answerDTO.UserId);

            if (quizExists == null || questionExists == null || userExists == null)
                return BadRequest("Invalid QuizId, QuestionId, or UserId. One or more provided references do not exist.");

            // Map the updated DTO values to the existing entity
            var answer = _mapper.Map(answerDTO, existingAnswer);

            // Update the answer and save it
            _answerRepository.Update(answer);
            _answerRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnswer(int id)
        {
            _answerRepository.Delete(id);
            _answerRepository.Save();
            return NoContent();
        }
    }
}

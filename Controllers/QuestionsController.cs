using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWorkShop.DTOs;
using QuizWorkShop.IRepository;
using QuizWorkShop.Models;

namespace QuizWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IGenericRepository<Question> questionsRepository;
        private readonly IMapper _mapper;
        public QuestionsController(IGenericRepository<Question> questions , IMapper mapper) 
        {
            questionsRepository = questions;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllQuestions() 
        {
            var questions = questionsRepository.GetAll();
            return Ok(questions);
        }
        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = questionsRepository.GetById(id);
            if (question == null) 
                return NotFound();
            return Ok(question);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult AddQuestion(QuestionDTO questionDTO)
        {
            if (questionDTO == null || !ModelState.IsValid)
                return BadRequest("Invalid data provided.");

            // Validate if the QuizId exists
            var quizExists = questionsRepository.GetAll().FirstOrDefault(q => q.QuizId == questionDTO.QuizId);
            if (quizExists == null)
                return BadRequest("Invalid QuizId. The specified quiz does not exist.");

            // Map the DTO to the Question entity
            var question = _mapper.Map<Question>(questionDTO);

            questionsRepository.Create(question);
            questionsRepository.Save();

            return CreatedAtAction("GetQuestionById", new { id = question.QuestionId }, question);
        }


        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult UpdateQuestion(int id, QuestionDTO questionDTO)
        {
            if (questionDTO == null || !ModelState.IsValid)
                return BadRequest("Invalid data provided.");

            var existingQuestion = questionsRepository.GetById(id);
            if (existingQuestion == null)
                return NotFound("The question does not exist.");

            // Validate if the new QuizId exists
            var quizExists = questionsRepository.GetAll().FirstOrDefault(q => q.QuizId == questionDTO.QuizId);
            if (quizExists == null)
                return BadRequest("Invalid QuizId. The specified quiz does not exist.");

            // Map the updated DTO values to the existing question
            var question = _mapper.Map(questionDTO, existingQuestion);
            questionsRepository.Update(question);
            questionsRepository.Save();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            questionsRepository.Delete(id);
            questionsRepository.Save();
            return NoContent();
        }
    }
}

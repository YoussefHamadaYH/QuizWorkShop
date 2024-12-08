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
    public class ChoicesController : ControllerBase
    {
        private readonly IGenericRepository<Choices> _ChoicesRepository;
        private readonly IMapper _mapper;
        public ChoicesController(IGenericRepository<Choices> ChoicesRepository , IMapper mapper)
        {
            _ChoicesRepository = ChoicesRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllChoices()
        {
            var Choices = _ChoicesRepository.GetAll();
            return Ok(Choices);
        }
        [HttpGet("{id}")]
        public IActionResult GetChoicesById(int id)
        {
            var Choice = _ChoicesRepository.GetById(id);
            if (Choice == null)
                return NotFound();
            return Ok(Choice);
        }
        [HttpPost]
        public IActionResult AddChoices(ChoiceDTO choiceDTO)
        {
            if (choiceDTO == null || !ModelState.IsValid)
                return BadRequest("Invalid data.");

            // Validate if the QuestionId exists
            var questionExists = _ChoicesRepository.GetAll().FirstOrDefault(q => q.QuestionId == choiceDTO.QuestionId);
            if (questionExists == null)
                return BadRequest("Invalid QuestionId. The specified question does not exist.");

            // Map DTO to the entity model
            var choice = _mapper.Map<Choices>(choiceDTO);

            _ChoicesRepository.Create(choice);
            _ChoicesRepository.Save();

            // Return the created choice as a DTO
            return CreatedAtAction("GetChoicesById", new { id = choice.ChoiceId }, choice);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateChoices(int id, ChoiceDTO choiceDTO)
        {
            if (!ModelState.IsValid || choiceDTO == null)
                return BadRequest("Invalid data.");

            var existingChoice = _ChoicesRepository.GetById(id);
            if (existingChoice == null)
                return NotFound("Choice not found.");

            // Validate if the new QuestionId exists
            var questionExists = _ChoicesRepository.GetAll().FirstOrDefault(q => q.QuestionId == choiceDTO.QuestionId);
            if (questionExists == null)
                return BadRequest("Invalid QuestionId. The specified question does not exist.");

            // Map the updated DTO values to the existing entity
            var choice = _mapper.Map(choiceDTO, existingChoice);

            _ChoicesRepository.Update(choice);
            _ChoicesRepository.Save();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteChoices(int id)
        {
            _ChoicesRepository.Delete(id);
            _ChoicesRepository.Save();
            return NoContent();
        }
    }
}

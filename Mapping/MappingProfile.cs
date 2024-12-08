using AutoMapper;
using QuizWorkShop.DTOs;
using QuizWorkShop.Models;

namespace QuizWorkShop.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
                // Mapping between QuizDTO and Quiz
                CreateMap<QuizDTO, Quiz>().ReverseMap();

                // Mapping between QuestionDTO and Question
                CreateMap<QuestionDTO, Question>().ReverseMap();

                // Mapping between AnswerDTO and Answer
                CreateMap<AnswerDTO, Answer>().ReverseMap();

                // If needed, map Question to QuestionDTO and Answer to AnswerDTO
                CreateMap<Quiz, QuizDTO>()
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                    .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

                CreateMap<Question, QuestionDTO>();
                CreateMap<Answer, AnswerDTO>();
        }
    }
}

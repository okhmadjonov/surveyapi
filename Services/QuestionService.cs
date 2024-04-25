using Microsoft.EntityFrameworkCore;
using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Entities;
using surveyapi.Exceptions;
using surveyapi.Extentions;
using surveyapi.Models;
using surveyapi.Repositories;
using System.Linq.Expressions;

namespace surveyapi.Services;

public class QuestionService : IQuestionRepository
{
    private readonly IGenericRepository<Question> _questionRepository;

    public QuestionService(IGenericRepository<Question> questionRepository)
    {
        _questionRepository = questionRepository;
    }
    public async ValueTask<QuestionModel> CreateAsync(QuestionDto qDto)
    {

        if (qDto == null)
        {
            throw new SurveyException(404, "question_cannot_be_empty");
        }

        var question = new Question
        {
            Text = qDto.Text,
            Type = qDto.Type,
            SurveyId = qDto.SurveyId,
        };
        var createdQuestion = await _questionRepository.CreateAsync(question);
        await _questionRepository.SaveChangesAsync();
        return new QuestionModel().MapFromEntity(createdQuestion);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findQuestion = await _questionRepository.GetAsync(p => p.Id == id);
        if (findQuestion is null)
        {
            throw new SurveyException(404, "question_not_found");
        }
        await _questionRepository.DeleteAsync(id);
        await _questionRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<QuestionModel>> GetAll(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
    {
        var questionsQuery = _questionRepository.GetAll(expression, isTracking: false)
                                              .Include(p => p.Choices);

        var questionsList = await questionsQuery.ToPagedList(@params).ToListAsync();

        return questionsList.Select(question => new QuestionModel().MapFromEntity(question)).ToList();
    }

    public async ValueTask<QuestionModel> GetAsync(Expression<Func<Question, bool>> expression)
    {
        var question = await _questionRepository.GetAsync(expression, false);
        if (question is null)
            throw new SurveyException(404, "question_not_found");
        return new QuestionModel().MapFromEntity(question);
    }

    public async ValueTask<QuestionModel> UpdateAsync(int id, QuestionDto qDto)
    {
        var existingQuestion = await _questionRepository.GetAsync(p => p.Id == id);

        if (existingQuestion == null)
        {
            throw new SurveyException(404, "question_not_found");
        }

        if (qDto.Text != null)
        {
            existingQuestion.Text = qDto.Text;
        }
        if (qDto.Type != null)
        {
            existingQuestion.Type = qDto.Type;
        }
        if (qDto.SurveyId != null)
        {
            existingQuestion.SurveyId = qDto.SurveyId;
        }

        await _questionRepository.SaveChangesAsync();
        return new QuestionModel().MapFromEntity(existingQuestion);
    }
}

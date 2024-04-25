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

public class SurveyService : ISurveyRepository
{
    private readonly IGenericRepository<Survey> _surveyRepository;
    private readonly IGenericRepository<UserSurvey> _userSurveyRepository;

    public SurveyService(IGenericRepository<Survey> surveyRepository, IGenericRepository<UserSurvey> userSurveyRepository)
    {
        _surveyRepository = surveyRepository;
        _userSurveyRepository = userSurveyRepository;
    }
    public async ValueTask<SurveyModel> CreateAsync(SurveyDto sDto, int UserId)
    {
        if (sDto == null)
        {
            throw new SurveyException(404, "survey_cannot_be_empty");
        }
        var survey = new Survey
        {
            Title = sDto.Title,
            Description = sDto.Description,
            EndDate = DateTime.SpecifyKind(sDto.EndDate, DateTimeKind.Utc),
           
        };
        var createdSurvey = await _surveyRepository.CreateAsync(survey);
        await _surveyRepository.SaveChangesAsync();

        var userSurvey = new UserSurvey
        {
            UserId = UserId,             
            SurveyId = createdSurvey.Id 
        };
        await _userSurveyRepository.CreateAsync(userSurvey);
        await _userSurveyRepository.SaveChangesAsync();
        return new SurveyModel().MapFromEntity(createdSurvey);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findSurvey = await _surveyRepository.GetAsync(p => p.Id == id);
        if (findSurvey is null)
        {
            throw new SurveyException(404, "survey_not_found");
        }
        await _surveyRepository.DeleteAsync(id);
        await _surveyRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<SurveyModel>> GetAll(PaginationParams @params, Expression<Func<Survey, bool>> expression = null)
    {
        var surveysQuery = _surveyRepository.GetAll(expression, isTracking: false)
                                                .Include(p => p.Questions);
                 
        var surveysList = await surveysQuery.ToPagedList(@params).ToListAsync();

        return surveysList.Select(survey => new SurveyModel().MapFromEntity(survey)).ToList();
    }

    public async ValueTask<SurveyModel> GetAsync(Expression<Func<Survey, bool>> expression)
    {
        var survey = await _surveyRepository.GetAsync(expression, false);
        if (survey is null)
            throw new SurveyException(404, "survey_not_found");
        return new SurveyModel().MapFromEntity(survey);
    }

    public async ValueTask<IEnumerable<SurveyModel>> GetSurveysByUserEmail(string email)
    {
        var userSurvey = await _userSurveyRepository.GetAsync(us => us.User.Email == email);

        if (userSurvey == null)
        {
            throw new SurveyException(404, "user_not_found");
        }

        var surveys =  _surveyRepository.GetAll(s => s.UserSurveys.Any(us => us.UserId == userSurvey.UserId));

        return surveys.Select(survey => new SurveyModel().MapFromEntity(survey));
    }

    public async ValueTask<SurveyModel> UpdateAsync(int id, SurveyDto sDto)
    {
        var existingSurvey = await _surveyRepository.GetAsync(p => p.Id == id);

        if (existingSurvey == null)
        {
            throw new SurveyException(404, "survey_not_found");
        }

        if (sDto.Title != null)
        {
           
            existingSurvey.Title = sDto.Title;
        }
        if (sDto.Description != null)
        { 
            existingSurvey.Description = sDto.Description;
        }
        if (sDto.EndDate != null)
        { 
            existingSurvey.EndDate = DateTime.SpecifyKind(sDto.EndDate, DateTimeKind.Utc);
        }
           
        await _surveyRepository.SaveChangesAsync();
        return new SurveyModel().MapFromEntity(existingSurvey);
    }
}

using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Entities;
using surveyapi.Models;
using System.Linq.Expressions;

namespace surveyapi.Repositories;

public interface ISurveyRepository
{
    ValueTask<IEnumerable<SurveyModel>> GetAll(PaginationParams @params, Expression<Func<Survey, bool>> expression = null);
    ValueTask<SurveyModel> GetAsync(Expression<Func<Survey, bool>> expression);
    ValueTask<SurveyModel> CreateAsync(SurveyDto sDto, int UserId);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<SurveyModel> UpdateAsync(int id, SurveyDto sDto);

    ValueTask<IEnumerable<SurveyModel>> GetSurveysByUserEmail(string email);
}

using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Entities;
using surveyapi.Models;
using System.Linq.Expressions;

namespace surveyapi.Repositories;

public interface IQuestionRepository
{
    ValueTask<IEnumerable<QuestionModel>> GetAll(PaginationParams @params, Expression<Func<Question, bool>> expression = null);
    ValueTask<QuestionModel> GetAsync(Expression<Func<Question, bool>> expression);
    ValueTask<QuestionModel> CreateAsync(QuestionDto qDto);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<QuestionModel> UpdateAsync(int id, QuestionDto qDto);
}

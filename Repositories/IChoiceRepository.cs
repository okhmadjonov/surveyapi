using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Dtos.User;
using surveyapi.Entities;
using surveyapi.Models;
using System.Linq.Expressions;

namespace surveyapi.Repositories;

public interface IChoiceRepository
{
    ValueTask<IEnumerable<ChoiceModel>> GetAll(PaginationParams @params, Expression<Func<Choice, bool>> expression = null);
    ValueTask<ChoiceModel> GetAsync(Expression<Func<Choice, bool>> expression);
    ValueTask<ChoiceModel> CreateAsync(ChoiceDto chDto);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<ChoiceModel> UpdateAsync(int id, ChoiceDto chDto);
}

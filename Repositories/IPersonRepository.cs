using surveyapi.Configuration;
using surveyapi.Dtos;
using surveyapi.Entities;
using surveyapi.Models;
using System.Linq.Expressions;

namespace surveyapi.Repositories;

public interface IPersonRepository
{
    ValueTask<IEnumerable<PersonModel>> GetAll(PaginationParams @params, Expression<Func<Person, bool>> expression = null);
    ValueTask<PersonModel> GetAsync(Expression<Func<Person, bool>> expression);
    ValueTask<PersonModel> CreateAsync(PersonDto personDto);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<PersonModel> UpdateAsync(int id, PersonDto personDto);
}

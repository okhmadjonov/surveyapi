using surveyapi.Configuration;
using surveyapi.Dtos.User;
using surveyapi.Entities;
using surveyapi.Models;
using System.Linq.Expressions;

namespace surveyapi.Repositories;

public interface IUserRepository
{
    ValueTask<IEnumerable<UserModel>> GetAll(PaginationParams @params, Expression<Func<User, bool>> expression = null);
    ValueTask<UserModel> GetAsync(Expression<Func<User, bool>> expression);
    ValueTask<UserModel> GetUserByEmail(string email);
    ValueTask<UserModel> CreateAsync(UserForCreationDto userForCreationDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<UserModel> UpdateAsync(int id, UserForCreationDto userForUpdateDTO);
}

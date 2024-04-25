using Microsoft.EntityFrameworkCore;
using surveyapi.Configuration;
using surveyapi.Dtos.User;
using surveyapi.Entities;
using surveyapi.Exceptions;
using surveyapi.Extentions;
using surveyapi.Models;
using surveyapi.Repositories;
using System.Linq.Expressions;
namespace surveyapi.Services;
internal sealed class UserService : IUserRepository
{
    private readonly IGenericRepository<User> _userRepository;
    public UserService(IGenericRepository<User> userRepository) => _userRepository = userRepository;
    public async ValueTask<UserModel> CreateAsync(UserForCreationDto user)
    {

        var existUser = await _userRepository.GetAsync(u => u.Email == user.Email);
        string passwordHash = existUser.Password.Encrypt();

        if (existUser != null)
        {
            existUser.Name = user.Name;
            existUser.Email = user.Email;
            existUser.Password = passwordHash;
        };
        var createdUser = await _userRepository.CreateAsync(existUser);
        await _userRepository.SaveChangesAsync();
        return new UserModel().MapFromEntity(createdUser);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findUser = await _userRepository.GetAsync(p => p.Id == id);
        if (findUser is null)
        {
            throw new SurveyException(404, "user_not_found");
        }
        await _userRepository.DeleteAsync(id);
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<UserModel>> GetAll(PaginationParams @params, Expression<Func<User, bool>> expression = null)
    {
        var users = _userRepository.GetAll(expression: expression, isTracking: false);
        var usersList = await users.ToPagedList(@params).ToListAsync();
        return usersList.Select(e => new UserModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<UserModel> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await _userRepository.GetAsync(expression);
        if (user is null) throw new SurveyException(404, "user_not_found");
        return new UserModel().MapFromEntity(user);
    }

    public async ValueTask<UserModel> GetUserByEmail(string email)
    {
        if (email == null) { 
            throw new SurveyException(404, "email_cannot_be_empty");
        }
        var user = await _userRepository.GetAsync(u=>u.Email==email);
        if (user is null) throw new SurveyException(404, "user_not_found");
        return new UserModel().MapFromEntity(user);
    }

    public async ValueTask<UserModel> UpdateAsync(int id, UserForCreationDto userForUpdateDTO)
    {
        var user = await _userRepository.GetAsync(u => u.Id == id);

        if (user is null)
            throw new SurveyException(404, "user_not_found");

        if (userForUpdateDTO.Email != user.Email)
            user.Email = userForUpdateDTO.Email;
        if (userForUpdateDTO.Name != user.Name)
            user.Name = userForUpdateDTO.Name;
        if (!string.IsNullOrEmpty(userForUpdateDTO.Password))
            user.Password = userForUpdateDTO.Password.Encrypt();
        await _userRepository.SaveChangesAsync();
        return new UserModel().MapFromEntity(user);
    }
}

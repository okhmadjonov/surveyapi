using surveyapi.Dtos.User;
using surveyapi.Models;

namespace surveyapi.Repositories;

public interface IAuthRepository
{
    ValueTask<UserModel> Registration(UserForCreationDto user);
    Task<string> Login(LoginDto loginDto);
}
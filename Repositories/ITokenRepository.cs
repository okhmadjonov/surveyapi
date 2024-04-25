using surveyapi.Entities;

namespace surveyapi.Repositories;

public interface ITokenRepository
{
    string CreateToken(User user, string role);
}
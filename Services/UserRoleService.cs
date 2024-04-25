using surveyapi.Entities;
using surveyapi.Exceptions;
using surveyapi.Repositories;
using System.Linq.Expressions;

namespace surveyapi.Services;

internal sealed class UserRoleService : IUserRoleRepository
{
    private readonly IGenericRepository<UserRole> _userroleRepository;
    public UserRoleService(IGenericRepository<UserRole> userroleRepository)
    {
        _userroleRepository = userroleRepository;
    }
    public async ValueTask<UserRole> GetAsync(Expression<Func<UserRole, bool>> expression)
    {
        var userrole = await _userroleRepository.GetAsync(expression);
        if (userrole == null)
        {
            throw new SurveyException(404, "user_role_not_found");
        }
        return userrole;
    }
}

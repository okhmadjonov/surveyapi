using surveyapi.Entities;
using surveyapi.Exceptions;
using surveyapi.Repositories;
using System.Linq.Expressions;

internal sealed class RoleService : IRoleRepository
{
    private readonly IGenericRepository<Role> _roleRepository;
    public RoleService(IGenericRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async ValueTask<Role> GetAsync(Expression<Func<Role, bool>> expression)
    {
        var role = await _roleRepository.GetAsync(expression);
        if (role == null)
        {
            throw new SurveyException(404, "role_not_found");
        }
        return role;
    }
}

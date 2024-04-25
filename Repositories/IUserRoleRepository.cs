using surveyapi.Entities;
using System.Linq.Expressions;
namespace surveyapi.Repositories;
public interface IUserRoleRepository
{
    ValueTask<UserRole> GetAsync(Expression<Func<UserRole, bool>> expression);
}

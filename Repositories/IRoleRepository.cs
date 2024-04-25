using surveyapi.Entities;
using surveyapi.Models;
using System.Linq.Expressions;

namespace surveyapi.Repositories;

public interface IRoleRepository
{
    ValueTask<Role> GetAsync(Expression<Func<Role, bool>> expression);
}

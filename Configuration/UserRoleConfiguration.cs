using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using surveyapi.Entities;

namespace surveyapi.Configuration;


internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(DefaultUserRoles);
    }
    private UserRole[] DefaultUserRoles =>
        new[]
        {
            new Entities.UserRole()
            {
                Id = 1,
                RoleId = 1,
                UserId = 1,
            },
        };
}

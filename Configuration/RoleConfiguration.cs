using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Data;
using surveyapi.Entities;
namespace surveyapi.Configuration;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(DefaultRoles);
    }
    private Role[] DefaultRoles = new[]
    {
        new Role("SuperAdmin")
        {
            Id = 1,
            IsActive = true
        },
         new Role("User")
        {
            Id = 2,
            IsActive = true
        },
    };
}

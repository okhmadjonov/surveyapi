using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using surveyapi.Entities;
using surveyapi.Extentions;
namespace surveyapi.Configuration;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(DefaultUserAdmin);
    }

    private User DefaultUserAdmin =>
        new User()
        {
            Id = 1,
            Name = "Admin",
            Email = "admin@gmail.com",
            Password = "Admin@123?".Encrypt(),
        };
}

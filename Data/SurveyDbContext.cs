using Microsoft.EntityFrameworkCore;
using surveyapi.Configuration;
using surveyapi.Entities;

namespace surveyapi.Data;

public class SurveyDbContext : DbContext
{
    public SurveyDbContext(DbContextOptions<SurveyDbContext> options): base(options)   { }

    public DbSet<User> Users { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<UserSurvey> UserSurveys { get; set; }
    public DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());


        modelBuilder.Entity<UserSurvey>()
                .HasKey(us => new { us.UserId, us.SurveyId });

        modelBuilder.Entity<UserSurvey>()
            .HasOne(us => us.User)
            .WithMany(u => u.UserSurveys)
            .HasForeignKey(us => us.UserId);

        modelBuilder.Entity<UserSurvey>()
            .HasOne(us => us.Survey)
            .WithMany(s => s.UserSurveys)
            .HasForeignKey(us => us.SurveyId);
    }
}

using surveyapi.Commons;

namespace surveyapi.Entities;

public class User : Auditable
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<UserSurvey> UserSurveys { get; set; }
}

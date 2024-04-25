using surveyapi.Commons;

namespace surveyapi.Entities;

public class UserSurvey : Auditable
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int SurveyId { get; set; }
    public Survey Survey { get; set; }
}

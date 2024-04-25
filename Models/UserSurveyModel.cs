using surveyapi.Entities;

namespace surveyapi.Models;

public class UserSurveyModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SurveyId { get; set; }

    public virtual UserSurveyModel MapFromEntity(UserSurvey entity)
    {
        Id = entity.Id;
        UserId = entity.UserId;
        SurveyId = entity.SurveyId;
        return this;
    }
}

using surveyapi.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace surveyapi.Entities;

public class Survey : Auditable
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
    public List<Question> Questions { get; set; }
    public ICollection<UserSurvey> UserSurveys { get; set; }
}
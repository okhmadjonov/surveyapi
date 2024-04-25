using surveyapi.Commons;
using surveyapi.Entities.enums;

namespace surveyapi.Entities;

public class Question : Auditable
{
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public List<Choice> Choices { get; set; } 
    public int? SurveyId { get; set; }
    public Survey Survey { get; set; }
}
using surveyapi.Commons;

namespace surveyapi.Entities;

public class Choice : Auditable
{
    public string Text { get; set; }
    public bool IsSelected { get; set; } = false;    
    public int? QuestionId { get; set; }
    public Question Question { get; set; }
}
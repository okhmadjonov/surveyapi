using surveyapi.Entities.enums;
using surveyapi.Entities;
using System.ComponentModel.DataAnnotations;

namespace surveyapi.Dtos;

public class QuestionDto
{
    [Required]
    public string Text { get; set; }
    [Required]
    public QuestionType Type { get; set; }

    [Required]
    public int SurveyId { get; set; }
    
}

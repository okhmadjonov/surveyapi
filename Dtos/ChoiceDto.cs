using System.ComponentModel.DataAnnotations;

namespace surveyapi.Dtos;

public class ChoiceDto
{
    [Required]
    public string Text { get; set; }

    [Required]
    public bool IsSelected { get; set; } = false;

    [Required]
    public int QuestionId { get; set; }
}

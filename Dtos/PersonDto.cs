using System.ComponentModel.DataAnnotations;

namespace surveyapi.Dtos;

public class PersonDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
}

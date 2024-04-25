using System.ComponentModel.DataAnnotations;

namespace surveyapi.Dtos
{
    public class SurveyDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EndDate { get; set; } 
    }
}

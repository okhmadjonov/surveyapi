using surveyapi.Entities;

namespace surveyapi.Models;

public class SurveyModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset EndDate { get; set; }
    //public List<QuestionModel> Questions { get; set; }


    public virtual SurveyModel MapFromEntity(Survey entity)
    {
        return new SurveyModel { 
        
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            EndDate = entity.EndDate,
            //Questions=entity.Questions !=null ? entity.Questions.Select(sur=> new QuestionModel().MapFromEntity(sur)).ToList() : null,    
        };
    }
}

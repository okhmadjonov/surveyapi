using surveyapi.Entities.enums;
using surveyapi.Entities;

namespace surveyapi.Models;

public class QuestionModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public List<ChoiceModel> Choices { get; set; }

    public virtual QuestionModel MapFromEntity(Question entity) {
        return new QuestionModel {
            Id = entity.Id,
            Text = entity.Text,
            Type = entity.Type,
            Choices = entity.Choices != null ? entity.Choices.Select(ch => new ChoiceModel().MapFromEntity(ch)).ToList() : null,
        };
    }
}


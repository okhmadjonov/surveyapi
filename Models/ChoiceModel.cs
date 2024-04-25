using surveyapi.Entities;

namespace surveyapi.Models;

public class ChoiceModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsSelected { get; set; } = false;

    public virtual ChoiceModel MapFromEntity(Choice entity) {
     return new ChoiceModel {
         Id = entity.Id, 
         Text = entity.Text,
         IsSelected = entity.IsSelected,
     };
    }
}

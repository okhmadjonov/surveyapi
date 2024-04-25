using surveyapi.Entities;

namespace surveyapi.Models;

public class PersonModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }


    public virtual PersonModel MapFromEntity(Person entity) {
        Id = entity.Id;
        Name = entity.Name;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        Email = entity.Email;
        Phone = entity.Phone;
        return this;
       
    }
}

using surveyapi.Entities;

namespace surveyapi.Models;

public class UserModel
{

    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual UserModel MapFromEntity(User entity)
    {
        Id = entity.Id;
        Username = entity.Name;
        Email = entity.Email;
        Password = entity.Password;
        return this;
    }
}
using surveyapi.Commons;

namespace surveyapi.Entities;

public class UserRole : Auditable
{ 
    public int UserId { get; set; }
    public User User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}

using surveyapi.Commons;

namespace surveyapi.Entities;

public class Role : Auditable
{
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public Role() { }
    public Role(string name)
    {
        Name = name;
    }
}

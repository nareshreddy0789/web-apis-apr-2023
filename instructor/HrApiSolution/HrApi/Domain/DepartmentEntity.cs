namespace HrApi.Domain;

public class DepartmentEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Removed { get; set; }
}
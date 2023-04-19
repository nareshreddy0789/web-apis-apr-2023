using HrApi.Models;

namespace HrApi.Domain;

public class HiringRequestEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public HiringRequestStatus Status { get; set; } = HiringRequestStatus.AwaitingSalary;
}

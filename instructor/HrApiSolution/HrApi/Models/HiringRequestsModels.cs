namespace HrApi.Models;

public record HiringRequestCreateModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    
}


public record HiringRequestResponseModel
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public HiringRequestStatus Status { get; set; } = HiringRequestStatus.AwaitingSalary;
}

public enum HiringRequestStatus {  AwaitingSalary, AwaitingDepartment, Hired, Declined }
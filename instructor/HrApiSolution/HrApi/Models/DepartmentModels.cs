namespace HrApi.Models;


public record DepartmentsResponse
{
    public List<DepartmentSummaryItem> Data { get; set; } = new();
}
public record DepartmentSummaryItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}



/*
 * {
    "data": [
        { "id": "1", "name": "Developers"},
        { "id": "2", "name": "Testers"},
    ]
}*/
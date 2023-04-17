
using Microsoft.AspNetCore.Mvc;
using HrApi.Models;

namespace HrApi.Controllers;



public class DepartmentsController : ControllerBase
{
    // GET /departments
    [HttpGet("/departments")]
    public async Task<ActionResult<DepartmentsResponse>> GetDepartments()
    {
        var response = new DepartmentsResponse
        {
            Data = new List<DepartmentSummaryItem> {
 new DepartmentSummaryItem { Id="1", Name = "Developers"},
 new DepartmentSummaryItem { Id="2", Name ="Testers"}
 }
        };
        return Ok(response);
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HrApi.Domain;
using HrApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrApi.Controllers;

public class DepartmentsController : ControllerBase
{

    private readonly HrDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public DepartmentsController(HrDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    [HttpPost("/departments")]
    public async Task<ActionResult> AddADepartment([FromBody] DepartmentCreateRequest request)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400

        }



        var departmentToAdd = _mapper.Map<DepartmentEntity>(request);
        _context.Departments.Add(departmentToAdd);
        try
        {
            await _context.SaveChangesAsync();
            var response = _mapper.Map<DepartmentSummaryItem>(departmentToAdd);
            return Ok(response);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("That Department Exists");
        }
    }


    // GET /departments
    [HttpGet("/departments")]
    public async Task<ActionResult<DepartmentsResponse>> GetDepartments()
    {
        var response = new DepartmentsResponse
        {
            Data = await _context.Departments
                .ProjectTo<DepartmentSummaryItem>(_config)
                .ToListAsync()
        };
        return Ok(response);
    }
}
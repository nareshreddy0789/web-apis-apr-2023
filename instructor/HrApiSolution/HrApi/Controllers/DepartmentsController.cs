using AutoMapper;
using AutoMapper.QueryableExtensions;
using HrApi.Domain;
using HrApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HrApi.Controllers;

[Route("/departments")]
[Produces("application/json")]
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

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateDepartment(int id, [FromBody] DepartmentUpdateRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var savedThingy = await _context.GetActiveDepartments().SingleOrDefaultAsync(d => d.Id == id);
        if (savedThingy is null)
        {
            return NotFound(); // or you could do an upsert
        }
        else
        {
            savedThingy.Name = request.Name; // could use automapper, etc. 
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    /// <summary>
    /// Use this to remove a department
    /// </summary>
    /// <param name="id">The id of the department to remove, duh.</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    public async Task<ActionResult> RemoveDepartment(int id)
    {
        var department = await _context.GetActiveDepartments()
            .SingleOrDefaultAsync(d => d.Id == id);
        if (department != null)
        {
            department.Removed = true;
            //_context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
        return NoContent(); // 204 - success, but no content (body)
    }

    [HttpPost()]
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
    public async Task<ActionResult<DepartmentSummaryItem>> AddADepartment([FromBody] DepartmentCreateRequest request)
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
            return CreatedAtRoute("get-department-by-id", new { id = response.Id }, response);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("That Department Exists");
        }
    }


    // GET /departments
    [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
    [HttpGet()]
    public async Task<ActionResult<DepartmentsResponse>> GetDepartments(CancellationToken token)
    {
        var response = new DepartmentsResponse
        {
            Data = await _context.GetActiveDepartments()
                .ProjectTo<DepartmentSummaryItem>(_config)
                .ToListAsync(token)
        };
        return Ok(response);
    }
    // GET /departments/8

    
    /// <summary>
    /// Allows you to retrieve a document with information about this department
    /// </summary>
    /// <param name="id">The id of the department</param>
    /// <returns>Either a 404 or some information about this.</returns>
    [HttpGet("{id:int}", Name = "get-department-by-id")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetDepartmentById(int id)
    {
   
        var response = await _context.GetActiveDepartments()
             .Where(dept => dept.Id == id)
             .ProjectTo<DepartmentSummaryItem>(_config)
             .SingleOrDefaultAsync();
        if (response is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }
}

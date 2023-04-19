using AutoMapper;
using HrApi.Domain;
using HrApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

public class HiringRequestsController :ControllerBase
{
    private readonly HrDataContext _context;
    private readonly IMapper _mapper;

    public HiringRequestsController(HrDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("/hiring-requests")]
    public async Task<ActionResult<HiringRequestResponseModel>> AddAHiringRequest([FromBody] HiringRequestCreateModel request)
    {
        // Validate it (validation attributes, all that jazz)
        var entity = _mapper.Map<HiringRequestEntity>(request);
        _context.HiringRequests.Add(entity);
        await _context.SaveChangesAsync();
        var response = _mapper.Map<HiringRequestResponseModel>(entity);
        // HiringRequestModel => HiringRequestEntity
        return Ok(response);
    }
}

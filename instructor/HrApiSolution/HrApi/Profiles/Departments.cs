using AutoMapper;
using HrApi.Domain;
using HrApi.Models;

namespace HrApi.Profiles;

public class Departments : Profile
{
    public Departments()
    {
       CreateMap<DepartmentCreateRequest, DepartmentEntity>();

        // Source is DepartmentEntity, Destination is DepartmentSummaryItem
        CreateMap<DepartmentEntity, DepartmentSummaryItem>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.ToString()));
    }
}

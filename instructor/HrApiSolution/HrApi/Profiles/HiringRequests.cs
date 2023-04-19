using AutoMapper;
using HrApi.Domain;
using HrApi.Models;

namespace HrApi.Profiles;

public class HiringRequests : Profile
{
    public HiringRequests()
    {
        CreateMap<HiringRequestEntity, HiringRequestResponseModel>();
        CreateMap<HiringRequestCreateModel, HiringRequestEntity>()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(_ => DateTime.Now));
    }
}

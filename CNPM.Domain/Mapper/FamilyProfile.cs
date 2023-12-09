using AutoMapper;

namespace CNPM.Domain.Mapper
{
    public class FamilyProfile : Profile
    {
        public FamilyProfile()
        {
            CreateMap<Family, FamilyDto>();
            CreateMap<Family, FamilyCUDto>();
            CreateMap<FamilyDto, Family>();
            CreateMap<FamilyCUDto, Family>();
        }
    }
}

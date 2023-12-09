using AutoMapper;

namespace CNPM.Domain
{
    public class FeeProfile : Profile
    {
        public FeeProfile()
        {
            CreateMap<Fee, FeeDto>();
            CreateMap<Fee, FeeCUDto>();
            CreateMap<FeeDto, Fee>();
            CreateMap<FeeCUDto, Fee>();
        }
    }
}

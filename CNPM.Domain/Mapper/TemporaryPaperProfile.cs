using AutoMapper;

namespace CNPM.Domain.Mapper
{
    public class TemporaryPaperProfile : Profile
    {
        public TemporaryPaperProfile()
        {
            CreateMap<TemporaryPaper, TemporaryPaperDto>();
            CreateMap<TemporaryPaper, TemporaryPaperCUDto>();
            CreateMap<TemporaryPaperDto, TemporaryPaper>();
            CreateMap<TemporaryPaperCUDto, TemporaryPaper>();
        }
    }
}

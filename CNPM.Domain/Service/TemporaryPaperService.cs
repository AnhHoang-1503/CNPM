using AutoMapper;

namespace CNPM.Domain
{
    public class TemporaryPaperService : BaseService<TemporaryPaper, TemporaryPaperDto, TemporaryPaperCUDto>
    {
        public TemporaryPaperService(ITemporaryPaperRepository temporaryPaperRepository, IMapper mapper) : base(temporaryPaperRepository, mapper)
        {
        }

        public override TemporaryPaper MapCUDtoToEntity(TemporaryPaperCUDto entityCUDto)
        {
            var temporaryPaper = _mapper.Map<TemporaryPaper>(entityCUDto);

            temporaryPaper.Date_of_issue = DateTime.Now;

            return temporaryPaper;
        }

        public override TemporaryPaper MapCUDtoToEntity(int id, TemporaryPaperCUDto entityCUDto)
        {
            var temporaryPaper = _mapper.Map<TemporaryPaper>(entityCUDto);

            temporaryPaper.Id = id;

            return temporaryPaper;
        }
    }
}

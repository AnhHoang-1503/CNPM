using AutoMapper;
using CNPM.Domain.Dto.Fee;
using CNPM.Domain.Entity;

namespace CNPM.Domain.Mapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, PersonCUDto>();
            CreateMap<PersonDto, Person>();
            CreateMap<PersonCUDto, Person>();
            CreateMap<NewbornCUDto, Person>();
        }
    }
}

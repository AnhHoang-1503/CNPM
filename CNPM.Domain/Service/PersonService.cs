using AutoMapper;
using CNPM.Domain.Dto.Fee;
using CNPM.Domain.Entity;

namespace CNPM.Domain.Service
{
    public class PersonService : BaseService<Person, PersonDto, PersonCUDto>
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personService, IMapper mapper) : base(personService, mapper)
        {
            _personRepository = personService;
        }

        public async Task<IEnumerable<Person>> FindPeopleByFamilyId(int familyId)
        {
            var people = await _personRepository.FindPeopleByFamilyId(familyId);

            return people;
        }

        public async Task CreateNewborn(NewbornCUDto newbornCUDto)
        {
            var newborn = _mapper.Map<Person>(newbornCUDto);

            newborn.Previous_permanent_residence = "Mới sinh";

            await _personRepository.Create(newborn);

            await _personRepository.SaveChangeAsync();
        }

        public async Task Die(int id)
        {
            var person = await _personRepository.GetById(id);

            person.Note = "Qua đời";

            await _personRepository.Update(id, person);

            await _personRepository.SaveChangeAsync();
        }

        public override Person MapCUDtoToEntity(PersonCUDto entityCUDto)
        {
            var person = _mapper.Map<Person>(entityCUDto);

            return person;
        }

        public override Person MapCUDtoToEntity(int id, PersonCUDto entityCUDto)
        {
            var person = _mapper.Map<Person>(entityCUDto);

            person.Id = id;

            return person;
        }
    }
}

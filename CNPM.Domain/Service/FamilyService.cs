using AutoMapper;
using CNPM.Domain.Entity;

namespace CNPM.Domain.Service
{
    public class FamilyService : BaseService<Family, FamilyDto, FamilyCUDto>
    {
        private readonly IPersonRepository _personRepository;

        private readonly IFamilyRepository _familyRepository;

        public FamilyService(IFamilyRepository familyRepository, IMapper mapper, IPersonRepository personRepository) : base(familyRepository, mapper)
        {
            _familyRepository = familyRepository;
            _personRepository = personRepository;
        }

        public async Task FamilySpilit(FamilySpilitDto familySpilitDto)
        {
            if (familySpilitDto.ListMemberIds == null)
            {
                throw new Exception("List member is null");
            }

            var newFamilyId = await _familyRepository.CurrentAutoIncrement();

            var newFamily = MapCUDtoToEntity(familySpilitDto.FamilyCUDto);

            await _familyRepository.Create(newFamily);

            var listPerson = await _personRepository.FindPeopleByListIds(familySpilitDto.ListMemberIds);

            foreach (var person in listPerson)
            {
                if (person.Family_id != familySpilitDto.Id)
                {
                    throw new Exception("Person not in family");
                }

                person.Family_id = newFamilyId;

                await _personRepository.Update(person.Id, person);
            }

            await _personRepository.SaveChangeAsync();

        }

        public override Family MapCUDtoToEntity(FamilyCUDto entityCUDto)
        {
            var family = _mapper.Map<Family>(entityCUDto);

            return family;
        }

        public override Family MapCUDtoToEntity(int id, FamilyCUDto entityCUDto)
        {
            var family = _mapper.Map<Family>(entityCUDto);

            family.Id = id;

            return family;
        }
    }
}

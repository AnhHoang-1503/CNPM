using AutoMapper;
using CNPM.Domain.Entity;
using CNPM.Domain.Interface.Repository;
using System.Runtime.Intrinsics.Arm;

namespace CNPM.Domain
{
    public class FeeService : BaseService<Fee, FeeDto, FeeCUDto>
    {
        private readonly IFeeRepository _feeRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IFamyliFeeMappingRepository _famyliFeeMappingRepository;
        private readonly IPersonRepository _personRepository;

        public FeeService(IFeeRepository feeRepository, IFamilyRepository familyRepository, IFamyliFeeMappingRepository famyliFeeMappingRepository, IPersonRepository personRepository, IMapper mapper) : base(feeRepository, mapper)
        {
            _feeRepository = feeRepository;
            _familyRepository = familyRepository;
            _famyliFeeMappingRepository = famyliFeeMappingRepository;
            _personRepository = personRepository;
        }

        public override async Task Create(FeeCUDto feeCUDto)
        {
            var feeEntity = MapCUDtoToEntity(feeCUDto);

            var family = await _familyRepository.GetAll();
            var familyIds = family.Select(x => x.Id).ToList();

            var feeId = await _feeRepository.CurrentAutoIncrement();

            await _feeRepository.Create(feeEntity);

            await _feeRepository.SaveChangeAsync();

            foreach (var familyId in familyIds)
            {
                var familyFeeMapping = new FamilyFeeMapping();
                familyFeeMapping.Family_id = familyId;
                familyFeeMapping.Fee_id = feeId;
                familyFeeMapping.Is_paid = false;
                familyFeeMapping.Date_of_payment = null;
                familyFeeMapping.Amount = feeCUDto.Amount;
                await _famyliFeeMappingRepository.Create(familyFeeMapping);
            }

            await _famyliFeeMappingRepository.SaveChangeAsync();
        }

        public async Task<decimal?> GetTotalAmount(int id)
        {
            var mapping = await _famyliFeeMappingRepository.FindByFeeId(id);

            var totalAmount = mapping.Sum(x => x.Amount);

            return totalAmount;
        }

        public async Task<List<FeeByFamilyDto>> GetFeeList(int id)
        {
            var mapping = await _famyliFeeMappingRepository.FindByFamilyId(id);

            var feeList = new List<FeeByFamilyDto>();

            foreach (var item in mapping)
            {
                var fee = await _feeRepository.FindById(item.Fee_id);
                var feeDto = _mapper.Map<FeeDto>(fee);
                feeList.Add(new FeeByFamilyDto()
                {
                    Amount = item.Amount,
                    Date_of_payment = item.Date_of_payment,
                    Family_id = item.Family_id,
                    Fee_id = item.Fee_id,
                    Is_paid = item.Is_paid,
                    Name = feeDto.Name,
                    Type = feeDto.Type
                });
            }

            return feeList;
        }

        public async Task CreateCleaningFee()
        {
            var feeEntity = new Fee() { Id = 0, Name = "Phí vệ sinh", Type = FeeType.Mandatory };

            var family = await _familyRepository.GetAll();
            var familyIds = family.Select(x => x.Id).ToList();

            var feeId = await _feeRepository.CurrentAutoIncrement();

            await _feeRepository.Create(feeEntity);

            await _feeRepository.SaveChangeAsync();

            foreach (var familyId in familyIds)
            {
                var familyMember = await _personRepository.FindPeopleByFamilyId(familyId);
                var familyMemberCount = familyMember.Count();
                var familyFeeMapping = new FamilyFeeMapping();
                familyFeeMapping.Family_id = familyId;
                familyFeeMapping.Fee_id = feeId;
                familyFeeMapping.Is_paid = false;
                familyFeeMapping.Date_of_payment = null;
                familyFeeMapping.Amount = 6000 * 12 * familyMemberCount;
                await _famyliFeeMappingRepository.Create(familyFeeMapping);
            }

            await _famyliFeeMappingRepository.SaveChangeAsync();
        }

        public override Fee MapCUDtoToEntity(FeeCUDto entityCUDto)
        {
            var fee = _mapper.Map<Fee>(entityCUDto);

            return fee;
        }

        public override Fee MapCUDtoToEntity(int id, FeeCUDto entityCUDto)
        {
            var fee = _mapper.Map<Fee>(entityCUDto);

            fee.Id = id;

            return fee;
        }
    }
}

using CNPM.Domain.Entity;

namespace CNPM.Domain
{
    public interface IFamyliFeeMappingRepository : IBaseRepository<FamilyFeeMapping>
    {
        Task<IEnumerable<FamilyFeeMapping>> FindByFamilyId(int familyId);

        Task<IEnumerable<FamilyFeeMapping>> FindByFeeId(int feeId);
    }
}

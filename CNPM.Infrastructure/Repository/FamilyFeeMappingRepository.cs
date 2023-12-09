using CNPM.Domain;
using CNPM.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Infrastructure
{
    public class FamilyFeeMappingRepository : BaseRepository<FamilyFeeMapping>, IFamyliFeeMappingRepository
    {
        public FamilyFeeMappingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FamilyFeeMapping>> FindByFamilyId(int familyId)
        {
            var familyFeeMappings = await _dbSet.Where(x => x.Family_id == familyId).ToListAsync();

            return familyFeeMappings;
        }

        public async Task<IEnumerable<FamilyFeeMapping>> FindByFeeId(int feeId)
        {
            var familyFeeMappings = await _dbSet.Where(x => x.Fee_id == feeId).ToListAsync();

            return familyFeeMappings;
        }
    }
}
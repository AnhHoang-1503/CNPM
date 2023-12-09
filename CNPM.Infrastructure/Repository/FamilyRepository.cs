using CNPM.Domain;

namespace CNPM.Infrastructure.Repository
{
    public class FamilyRepository : BaseRepository<Family>, IFamilyRepository
    {
        public FamilyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

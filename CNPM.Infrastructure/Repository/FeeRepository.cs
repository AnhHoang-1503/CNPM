using CNPM.Domain;
using CNPM.Domain.Interface.Repository;

namespace CNPM.Infrastructure
{
    public class FeeRepository : BaseRepository<Fee>, IFeeRepository
    {
        public FeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

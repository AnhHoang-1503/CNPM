using CNPM.Domain;

namespace CNPM.Infrastructure
{
    public class TemporaryPaperRepository : BaseRepository<TemporaryPaper>, ITemporaryPaperRepository
    {
        public TemporaryPaperRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

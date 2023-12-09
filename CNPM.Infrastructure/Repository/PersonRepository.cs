using CNPM.Domain;
using CNPM.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Person>> FindPeopleByFamilyId(int familyId)
        {
            var people = await _dbSet.Where(x => x.Family_id == familyId).ToListAsync();

            return people;
        }

        public async Task<IEnumerable<Person>> FindPeopleByListIds(IEnumerable<int> ids)
        {
            var people = await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();

            return people;
        }
    }
}

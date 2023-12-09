using CNPM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Infrastructure
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> FindByUserName(string userName)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == userName);

            return user;
        }

    }
}

namespace CNPM.Domain
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindByUserName(string userName);
    }
}

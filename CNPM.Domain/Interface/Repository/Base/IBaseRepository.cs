namespace CNPM.Domain
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lay tat ca ban ghi
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Lay ban ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(int id);

        /// <summary>
        /// Tim kiem theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> FindById(int id);

        /// <summary>
        /// Tao ban ghi moi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(TEntity entity);

        /// <summary>
        /// Chinh sua ban ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(int id, TEntity entity);

        /// <summary>
        /// Xoa ban ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Lay id tu dong tang
        /// </summary>
        /// <returns></returns>
        Task<int> CurrentAutoIncrement();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <returns></returns>
        Task SaveChangeAsync();

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollbackTransaction();
    }
}

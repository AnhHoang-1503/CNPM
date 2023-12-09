using CNPM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace CNPM.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<int> CurrentAutoIncrement()
        {
            var tableName = _dbSet.EntityType.GetTableName();
            var connection = _context.Database.GetDbConnection();

            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'cnpm' AND TABLE_NAME = '{tableName}'";
                var result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    int autoIncrementValue = Convert.ToInt32(result);
                    return autoIncrementValue;
                }
            }
            return -1;
        }

        public async Task Delete(int id)
        {
            var entity = await FindById(id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }

            _dbSet.Remove(entity);
        }

        public async Task<TEntity?> FindById(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var entities = await _dbSet.ToListAsync();

            return entities;
        }

        public async Task<TEntity> GetById(int id)
        {
            var entity = await FindById(id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }

            return entity;
        }

        public async Task Update(int id, TEntity entity)
        {
            var existingEntity = await GetById(id);

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public async Task RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}

using FunWithFoodDomain.Interfaces.Common;
using FunWithFoodDomain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace FunWithFoodInfrastructure.Repositories.Common
{
    public class BaseGenericRepository<T> : IBaseGenericRepository<T> where T : BaseEntity
    {
        public readonly FoodDbContext dbContext;
        public readonly DbSet<T> _dbEntitySet;

        public BaseGenericRepository(FoodDbContext dbContext)
        {
            this.dbContext = dbContext;
            _dbEntitySet = dbContext.Set<T>();
        }

        public IQueryable<T> GetEntityQuery()
        {
            return _dbEntitySet.AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            await _dbEntitySet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbEntitySet.AddRangeAsync(entities);
        }

        public async Task<T?> GetByIdAsync(Guid id, bool withTracking)
        {
            return withTracking ? await _dbEntitySet.FirstOrDefaultAsync(x => x.Id == id) :
                                  await _dbEntitySet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, bool withTracking)
        {
            return withTracking ? await _dbEntitySet.Where(x => ids.Contains(x.Id)).ToListAsync() :
                                  await _dbEntitySet.AsNoTracking().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByListAsync(IQueryable<T> query, bool withTracking)
        {
            return withTracking ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFirstOrDefaultAsync(IQueryable<T> query, bool withTracking)
        {
            return withTracking ? await query.FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T?> GetByFirstAsync(IQueryable<T> query, bool withTracking)
        {
            return withTracking ? await query.FirstAsync() : await query.AsNoTracking().FirstAsync();
        }

        // for classes that DO NOT inherit BaseEntity
        public async Task<IEnumerable<T>> GetListAsync<T>(IQueryable<T> query, bool withTracking) where T : class
        {
            return withTracking ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
        }
        // for classes that DO NOT inherit BaseEntity
        public async Task<T?> GetFirstOrDefaultAsync<T>(IQueryable<T> query, bool withTracking) where T : class
        {
            return withTracking ? await query.FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            _dbEntitySet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbEntitySet.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _dbEntitySet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbEntitySet.RemoveRange(entities);
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

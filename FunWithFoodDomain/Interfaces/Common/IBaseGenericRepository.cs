using FunWithFoodDomain.Models.Common;

namespace FunWithFoodDomain.Interfaces.Common
{
    public interface IBaseGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetEntityQuery();
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T?> GetByIdAsync(Guid id, bool withTracking);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, bool withTracking);
        Task<IEnumerable<T>> GetByListAsync(IQueryable<T> query, bool withTracking);
        Task<T?> GetByFirstOrDefaultAsync(IQueryable<T> query, bool withTracking);
        Task<T?> GetByFirstAsync(IQueryable<T> query, bool withTracking);
        Task<IEnumerable<T>> GetListAsync<T>(IQueryable<T> query, bool withTracking) where T : class;
        Task<T?> GetFirstOrDefaultAsync<T>(IQueryable<T> query, bool withTracking) where T : class;
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

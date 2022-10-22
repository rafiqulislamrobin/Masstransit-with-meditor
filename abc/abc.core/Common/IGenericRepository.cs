using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc.core.Common
{
    public interface IGenericRepository<T, in TKey> where T : Entity<TKey>
    {
        IQueryable<T> Query();

        Task<T> GetByIdAsync(TKey id, string includeProperties = "");

        Task<IReadOnlyList<T>> ListAllAsync();

        Task AddAsync(T entity);

        void AddRangeAsync(List<T> entities);

        void Update(T entity);

        void UpdateRange(List<T> entities);

        void Delete(T entity);
    }
}

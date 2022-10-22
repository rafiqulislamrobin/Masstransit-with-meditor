using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc.core.Common
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>;

        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}

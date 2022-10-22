using abc.core.Common;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace abc.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : Entity<TKey>
        {
            _repositories ??= new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new GenericRepository<TEntity, TKey>(_context);
            return (IGenericRepository<TEntity, TKey>)_repositories[type];
        }
    }
}

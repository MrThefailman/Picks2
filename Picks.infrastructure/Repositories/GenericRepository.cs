using Microsoft.EntityFrameworkCore;
using Picks.core;
using Picks.infrastructure.Data;
using Picks.infrastructure.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(TEntity entity)
        {
            entity.Created = DateTime.Now;
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> Find()
        {
            return _context.Set<TEntity>().AsNoTracking()
                .OrderByDescending(x => x.Created);
        }
    }
}

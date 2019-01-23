using Microsoft.EntityFrameworkCore;
using Picks.core.Entities;
using Picks.infrastructure.Data;
using Picks.infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picks.infrastructure.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await Find().ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await Find().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

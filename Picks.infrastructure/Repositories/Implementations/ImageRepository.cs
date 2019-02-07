using Microsoft.EntityFrameworkCore;
using Picks.core.Entities;
using Picks.infrastructure.Data;
using Picks.infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Picks.infrastructure.Repositories.Implementations
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ImageRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Image>> Get()
        {
            return await Find().ToListAsync();
        }
        
        public async Task<Image> GetById(int id)
        {
            return await FindById(id);
        }

        public async Task<IEnumerable<Image>> GetByCategoryId(int categoryId)
        {
            return await Find().Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}

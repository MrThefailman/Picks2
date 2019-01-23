using Picks.core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picks.infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> Get();
    }
}

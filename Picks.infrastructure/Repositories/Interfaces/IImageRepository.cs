using Microsoft.AspNetCore.Http;
using Picks.core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picks.infrastructure.Repositories.Interfaces
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task<IEnumerable<Image>> Get();
        Task<IEnumerable<Image>> GetByCategoryId(int categoryId);
    }
}

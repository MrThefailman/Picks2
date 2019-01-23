using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        IQueryable<TEntity> Find();
    }
}

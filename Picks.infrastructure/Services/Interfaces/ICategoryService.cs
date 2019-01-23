using Picks.core.Entities;
using Picks.infrastructure.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picks.infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> Get();
        Task Add(CategoryViewModel vm);
    }
}

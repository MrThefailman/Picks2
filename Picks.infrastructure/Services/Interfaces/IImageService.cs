using Picks.infrastructure.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picks.infrastructure.Services.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<ImageViewModel>> Get();
        Task<IEnumerable<ImageViewModel>> GetByCategoryId(int categoryId);
        Task Add(ImageViewModel vm);
    }
}

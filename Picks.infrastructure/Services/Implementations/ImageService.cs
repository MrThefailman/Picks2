using AutoMapper;
using Picks.core.Entities;
using Picks.infrastructure.Repositories.Interfaces;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.Services.Implementations
{
    public class ImageService : IImageService
    {
        public readonly IImageRepository _imageRepo;
        public readonly IMapper _mapper;
        public ImageService(
            IImageRepository imageRepo,
            IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task Add(ImageViewModel vm)
        {
            await _imageRepo.Add(_mapper.Map<Image>(vm));
        }

        public async Task<IEnumerable<ImageViewModel>> Get()
        {
            return _mapper.Map<IEnumerable<ImageViewModel>>(await _imageRepo.Get());
        }

        public async Task<IEnumerable<ImageViewModel>> GetByCategoryId(int categoryId)
        {
            var Images = await _imageRepo.Get();

            return _mapper.Map<IEnumerable<ImageViewModel>>(Images.Where(x => x.CategoryId == categoryId));

        }
    }
}

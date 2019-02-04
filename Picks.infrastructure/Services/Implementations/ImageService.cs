using AutoMapper;
using Microsoft.AspNetCore.Http;
using Picks.core.Entities;
using Picks.infrastructure.Helpers;
using Picks.infrastructure.Repositories.Interfaces;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.Services.Implementations
{
    public class ImageService : IImageService
    {
        public readonly IImageRepository _imageRepo;
        public readonly IMapper _mapper;
        private readonly AzureStorageConfig _azureStorageConfig;
        public readonly UploadImageHelper _imageHelper;
        
        public ImageService(
            IImageRepository imageRepo,
            IMapper mapper,
            AzureStorageConfig azureStorageConfig,
            UploadImageHelper imageHelper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
            _imageHelper = imageHelper;
            _azureStorageConfig = azureStorageConfig;
        }

        public async Task Add(AddImageViewModel vm)
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

        public async Task<Uri> UploadImage(IFormFileCollection files, int categoryId)
        {
            foreach (var file in files)
            {
                if (UploadImageHelper.IsImage(file))
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    //var blobUri = await _imageHelper.UploadImage(file, fileName, file.ContentType, _azureStorageConfig);
                    var blobUri = await UploadImageHelper.UploadImage(file, fileName, file.ContentType, _azureStorageConfig);
                    if(blobUri != null)
                    {

                    }
                }
                
            }
            return null;
        }
    }
}

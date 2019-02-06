using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Picks.core.Entities;
using Picks.infrastructure.Helpers;
using Picks.infrastructure.Repositories.Interfaces;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.Services.Implementations
{
    public class ImageService : IImageService
    {
        public readonly IImageRepository _imageRepo;
        public readonly ICategoryRepository _categoryRepo;
        public readonly IMapper _mapper;
        public readonly IHostingEnvironment _env;
        public readonly IConfiguration _config;
        private readonly AzureStorageConfig _azureStorageConfig;
        public readonly UploadImageHelper _imageHelper;

        public ImageService(
            IImageRepository imageRepo,
            ICategoryRepository categoryRepo,
            IMapper mapper,
            IHostingEnvironment env,
            IConfiguration config,
            IOptions<AzureStorageConfig> azureStorageConfig,
            UploadImageHelper imageHelper)
        {
            _imageRepo = imageRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _env = env;
            _config = config;
            _imageHelper = imageHelper;
            _azureStorageConfig = azureStorageConfig.Value;
            _env = env;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("Picks"));
            }
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

        public async Task<UploadResult> UploadImage(IFormFileCollection files, AddImageViewModel vm)
        {
            var result = new UploadResult();
            foreach (var file in files)
            {
                if (UploadImageHelper.IsImage(file))
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    // Uploads image to blobStorage
                    var blobUri = await UploadImageHelper.UploadImage(file, fileName, file.ContentType, _azureStorageConfig);
                    if (blobUri != null)
                    {
                        try
                        {
                            var img = new Image()
                            {
                                CategoryId = vm.CategoryId,
                                Name = vm.Name,
                                Created = DateTime.Now,
                                Path = blobUri.AbsoluteUri
                            };

                            await _imageRepo.Add(img);
                        }
                        catch (Exception err)
                        {
                            return null;
                        }
                    }
                }

            }
            return result;
        }
    }
}

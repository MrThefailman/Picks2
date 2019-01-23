using AutoMapper;
using Picks.core.Entities;
using Picks.infrastructure.ViewModels;

namespace Picks.infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Image, ImageViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}

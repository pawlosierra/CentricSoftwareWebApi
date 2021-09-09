using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.DTOs;

namespace CentricSoftwareWebApi.Mappers
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<ProductRequest, Product>()
        .ForPath(dest => dest.IdClothingNavigation.Name, opt => opt.MapFrom(src => src.Name))
        .ForPath(dest => dest.IdClothingNavigation.Description, opt => opt.MapFrom(src => src.Description))
        .ForPath(dest => dest.IdClothingNavigation.Brand, opt => opt.MapFrom(src => src.Brand))
        .ForPath(dest => dest.IdClothingNavigation.Category, opt => opt.MapFrom(src => src.Category))
        .ForPath(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

      CreateMap<Product, ProductResponse>()
        .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.IdClothingNavigation.Id))
        .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.IdClothingNavigation.Name))
        .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.IdClothingNavigation.Description))
        .ForPath(dest => dest.Category, opt => opt.MapFrom(src => src.IdClothingNavigation.Category))
        .ForPath(dest => dest.Created_at, opt => opt.MapFrom(src => $"{src.IdClothingNavigation.CreatedAt.ToString("s")}Z"))
        .ForPath(dest => dest.Brand, opt => opt.MapFrom(src => src.IdClothingNavigation.Brand))
        .ForPath(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
    }
  }
}

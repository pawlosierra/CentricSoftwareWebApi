using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Infrastructure.Models;

namespace CentricSoftwareWebApi.Infrastructure.Mappers
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<ProductModel, Product>().ReverseMap();
    }
  }
}

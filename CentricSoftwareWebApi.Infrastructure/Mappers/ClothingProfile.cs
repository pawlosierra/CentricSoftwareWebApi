using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Infrastructure.Models;

namespace CentricSoftwareWebApi.Infrastructure.Mappers
{
  public class ClothingProfile : Profile
  {
    public ClothingProfile()
    {
      CreateMap<ClothingModel, Clothing>().ReverseMap();
    }
  }
}

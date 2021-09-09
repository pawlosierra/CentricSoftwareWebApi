using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.DTOs;

namespace CentricSoftwareWebApi.Mappers
{
  public class ClothingProfile : Profile
  {
    public ClothingProfile()
    {
      CreateMap<Clothing, ClothingResponse>().ReverseMap()
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
          .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
          .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
          .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
          .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand));


    }
  }
}

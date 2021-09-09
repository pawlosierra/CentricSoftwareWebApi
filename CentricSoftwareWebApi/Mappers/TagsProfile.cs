using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.DTOs;

namespace CentricSoftwareWebApi.Mappers
{
  public class TagsProfile : Profile
  {
    public TagsProfile()
    {
      CreateMap<Tags, TagsResponse>().ReverseMap()
        .ForPath(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
  }
}

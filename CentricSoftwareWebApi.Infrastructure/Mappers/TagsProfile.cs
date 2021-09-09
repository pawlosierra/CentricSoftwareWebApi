using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Infrastructure.Models;

namespace CentricSoftwareWebApi.Infrastructure.Mappers
{
  public class TagsProfile : Profile
  {
    public TagsProfile()
    {
      CreateMap<Tags, TagsModel>()
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
      CreateMap<TagsModel, Tags>();
    }
  }
}

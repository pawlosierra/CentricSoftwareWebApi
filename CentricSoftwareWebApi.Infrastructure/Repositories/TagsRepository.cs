using AutoMapper;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Domain.Repositories;
using CentricSoftwareWebApi.Infrastructure.Data;
using CentricSoftwareWebApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Infrastructure.Repositories
{
  public class TagsRepository : ITagsRepository
  {
    private readonly productsContext _productsContext;
    private readonly IMapper _mapper;

    public TagsRepository(
      productsContext productsContext, 
      IMapper mapper)
    {
      _productsContext = productsContext;
      _mapper = mapper;
    }

    public async Task<Tags> AddTags(Tags tagsRequest)
    {
      //Before adding the Tag I verify that it does not exist in the database
      var lstTags = await _productsContext.TagsModels.ToListAsync();
      if (!lstTags.Any(x => x.Description.Trim() == tagsRequest.Description))
      {
        await _productsContext.TagsModels
          .AddAsync(_mapper.Map<TagsModel>(tagsRequest));
        await _productsContext.SaveChangesAsync();
      }
      return tagsRequest;
    }

    public async Task<IEnumerable<Tags>> GetTags()
    {
      var tags = await _productsContext.TagsModels
        .OrderBy(lts => lts.Id).ToListAsync();
      return _mapper.Map<IEnumerable<Tags>>(tags);
    }
  }
}

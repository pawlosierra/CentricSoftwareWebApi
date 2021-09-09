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
  public class ClothingRepository : IClothingRepository
  {
    private readonly productsContext _productsContext;
    private readonly IMapper _mapper;
    public ClothingRepository(
      productsContext productsContext,
      IMapper mapper)
    {
      _productsContext = productsContext;
      _mapper = mapper;
    }

    public async Task<Clothing> AddClothing(Clothing clothing)
    {
      clothing.Id = Guid.NewGuid();
      var addedClothing = await _productsContext.ClothingModels
        .AddAsync(_mapper.Map<ClothingModel>(clothing));
      await _productsContext.SaveChangesAsync();
      return clothing;
    }
  }
}

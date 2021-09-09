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
  public class ProductRepository : IProductRepository
  {
    private readonly productsContext _productsContext;
    private readonly IMapper _mapper;

    public ProductRepository(
      productsContext productsContext, 
      IMapper mapper)
    {
      _productsContext = productsContext;
      _mapper = mapper;
    }

    public async Task<Product> AddProduct(Product product)
    {
      await _productsContext.ProductModels
        .AddAsync(_mapper.Map<ProductModel>(product));
      await _productsContext.SaveChangesAsync();
      return product;
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string category)
    {
      var product = await _productsContext.ProductModels
        .Include(x => x.IdClothingNavigation)
        .Include(x => x.IdTagsNavigation)
        .Where(y => y.IdClothingNavigation.Category == category)
        .ToListAsync();
      return _mapper.Map<IEnumerable<Product>>(product);
    }

    public async Task<IEnumerable<Product>> GetProductById(Guid productId)
    {
      var product = await _productsContext.ProductModels
        .Include(x => x.IdClothingNavigation)
        .Include(x => x.IdTagsNavigation)
        .Where(y => y.IdClothing == productId)
        .ToListAsync();
      return _mapper.Map<IEnumerable<Product>>(product);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      var products = await _productsContext.ProductModels
       .Include(x => x.IdClothingNavigation)
       .Include(x => x.IdTagsNavigation)
       .ToListAsync();
      return _mapper.Map<IEnumerable<Product>>(products);
    }
  }
}

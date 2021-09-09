using CentricSoftwareWebApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Domain.Repositories
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductByCategory(string category);
    Task<IEnumerable<Product>> GetProductById(Guid productId);
    Task<Product> AddProduct(Product product);
  }
}

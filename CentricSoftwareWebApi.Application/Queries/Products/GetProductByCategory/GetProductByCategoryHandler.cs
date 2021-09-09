using CentricSoftwareWebApi.Application.Queries.Products.GetProductsById;
using CentricSoftwareWebApi.Domain.Exceptions;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Application.Queries.Products.GetProductById
{
  public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategory, IEnumerable<Product>>
  {
    private readonly IProductRepository _productRepository;

    public GetProductByCategoryHandler(IProductRepository productRepository)
    {
      _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductByCategory request, CancellationToken cancellationToken)
    {
      var products = await _productRepository.GetProductByCategory(request.Category);
      if (products == null)
      {
        throw new ProductException("AN ERROR OCCURRED DURING THE REQUEST",
                                    "The error occurred getting the products by Id.");
      }
      var lstProducts = products.GroupBy(x => x.IdClothing).Select(y => y.First()).ToList();

      foreach (var product in lstProducts)
      {
        var lstTags = new List<string>();
        foreach (var item in product.IdClothingNavigation.ProductModels)
        {
          lstTags.Add(item.IdTagsNavigation.Description.Trim());
        }
        product.Tags = lstTags;
      }
      return lstProducts.OrderByDescending(x => x.IdClothingNavigation.CreatedAt);
    }
  }
}

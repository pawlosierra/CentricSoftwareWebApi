using CentricSoftwareWebApi.Domain.Exceptions;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Application.Queries.Products.GetProducts
{
  public class GetProductsHandler : IRequestHandler<GetProducts, IEnumerable<Product>>
  {
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(
      IProductRepository clothingTagsRepository)
    {
      _productRepository = clothingTagsRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProducts request, CancellationToken cancellationToken)
    {
      var products = await _productRepository.GetProducts();
      if (products == null)
      {
        throw new ProductException("AN ERROR OCCURRED DURING THE REQUEST",
                                    "The error occurred getting the products");
      }
      var result = products.GroupBy(x => x.IdClothing).Select(y => y.First()).ToList();

      foreach (var product in result)
      {
        var temp = new List<string>();
        foreach (var item in product.IdClothingNavigation.ProductModels)
        {
          temp.Add(item.IdTagsNavigation.Description.Trim());
        }
        product.Tags = temp;
      }
      return result;
    }
  }
}

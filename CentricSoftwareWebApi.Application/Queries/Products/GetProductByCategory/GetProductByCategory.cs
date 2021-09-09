using CentricSoftwareWebApi.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace CentricSoftwareWebApi.Application.Queries.Products.GetProductsById
{
  public class GetProductByCategory : IRequest<IEnumerable<Product>>
  {
    public GetProductByCategory(string category)
    {
      Category = category;
    }

    public string Category { get; set; }
  }
}

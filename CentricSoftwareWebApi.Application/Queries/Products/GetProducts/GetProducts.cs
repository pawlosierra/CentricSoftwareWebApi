using CentricSoftwareWebApi.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace CentricSoftwareWebApi.Application.Queries.Products.GetProducts
{
  public class GetProducts : IRequest<IEnumerable<Product>>
  {
  }
}

using CentricSoftwareWebApi.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CentricSoftwareWebApi.Application.Commands.Products.AddProduct
{
  public class AddProduct : IRequest<Product>
  {
    public AddProduct(Product product)
    {
      Product = product;
      Product.IdClothingNavigation.CreatedAt = DateTime.Parse($"{DateTime.UtcNow.ToString("s")}Z", CultureInfo.InvariantCulture);
    }

    public Product Product { get; set; }
  }
}

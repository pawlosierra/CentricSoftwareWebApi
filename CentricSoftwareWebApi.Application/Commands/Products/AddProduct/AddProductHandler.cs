using CentricSoftwareWebApi.Domain.Exceptions;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Application.Commands.Products.AddProduct
{
  public class AddProductHandler : IRequestHandler<AddProduct, Product>
  {
    private readonly IProductRepository _productRepository;
    private readonly IClothingRepository _clothingRepository;
    private readonly ITagsRepository _tagsRepository;
    private readonly Clothing _clothing;
    private readonly Tags _tags;
    private readonly Product _product;

    public AddProductHandler(IProductRepository productRepository, IClothingRepository clothingRepository, ITagsRepository tagsRepository)
    {
      _productRepository = productRepository;
      _clothingRepository = clothingRepository;
      _tagsRepository = tagsRepository;
      _clothing = new Clothing();
      _tags = new Tags();
      _product = new Product();
    }

    //There are three tables in the Products database: ClothingModel, TagsModel and ProductModel.In this last one I have two foreign keys that relate ClothingModel and TagsModel.

    public async Task<Product> Handle(AddProduct request, CancellationToken cancellationToken)
    {
      //First I add the Tags.
      //request.Product.Tags is the list of Tags that I am going to add to my database.
      foreach (var tag in request.Product.Tags)
      {
        _tags.Description = tag;
        var addedTag = await _tagsRepository.AddTags(_tags);
        if (addedTag == null)
        {
          throw new TagsException("AN ERROR OCCURRED DURING THE REQUEST",
                                  "The error occurred adding the tags. You cannot insert empty fields.");
        }
      }
      var clothingCreation = ClothingCreation(request.Product);
      //second I add the Clothing
      var addedClothing = await _clothingRepository.AddClothing(clothingCreation);
      if (addedClothing == null)
      {
        throw new ClothingException("AN ERROR OCCURRED DURING THE REQUEST",
                                "The error occurred adding the clothing");
      }
      var tagsUpdated = await _tagsRepository.GetTags();
      //I use the list request.Product.Tags to add the Tag description with the respective IdClothing. This last one will be repeated depending on the amount of items we have in the list request.Product.Tags.
      foreach (var tag in request.Product.Tags)
      {
        if (tagsUpdated.Any(x => x.Description.Trim() == tag))
        {
          _product.IdClothing = clothingCreation.Id;
          var temp = tagsUpdated.Where(x => x.Description.Trim() == tag).Select(y => y.Id).First();
          _product.IdTags = temp;
          //Finally I add a new item to the product table
          var addedProduct = await _productRepository.AddProduct(_product);
          if (addedProduct == null)
          {
            throw new ProductException("AN ERROR OCCURRED DURING THE REQUEST",
                                "The error occurred adding the product");
          }
        }
      }
      //To show the product that was added I perform a search using the Clothing table using the IdClothing.
      var products = await _productRepository.GetProductById(clothingCreation.Id);
      var product = products.First();
      var lstTags = new List<string>();
      //All tags are added to the new product created.
      foreach (var item in product.IdClothingNavigation.ProductModels)
      {
        lstTags.Add(item.IdTagsNavigation.Description.Trim());
      }
      product.Tags = lstTags;
      return product;
    }

    private Clothing ClothingCreation(Product product)
    {
      _clothing.Name = product.IdClothingNavigation.Name;
      _clothing.Description = product.IdClothingNavigation.Description;
      _clothing.Category = product.IdClothingNavigation.Category;
      _clothing.CreatedAt = product.IdClothingNavigation.CreatedAt;
      _clothing.Brand = product.IdClothingNavigation.Brand;
      return _clothing;
    }
  }
}

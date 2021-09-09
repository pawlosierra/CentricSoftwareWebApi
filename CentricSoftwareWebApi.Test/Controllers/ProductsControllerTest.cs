using AutoFixture;
using AutoMapper;
using CentricSoftwareWebApi.Application.Commands.Products.AddProduct;
using CentricSoftwareWebApi.Application.Queries.Products.GetProducts;
using CentricSoftwareWebApi.Application.Queries.Products.GetProductsById;
using CentricSoftwareWebApi.Controllers;
using CentricSoftwareWebApi.Domain.Exceptions;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.DTOs;
using CentricSoftwareWebApi.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CentricSoftwareWebApi.Test.Controllers
{
  public class ProductsControllerTest
  {
    private readonly CancellationToken _cancellationToken;
    private readonly Mock<ILogger<ProductsController>> _loggerMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;

    private readonly ProductsController _tested;

    public ProductsControllerTest()
    {
      _cancellationToken = default;
      _loggerMock = new Mock<ILogger<ProductsController>>();
      _mediatorMock = new Mock<IMediator>();

      var configuration = new MapperConfiguration(cfg => 
      {
        cfg.AddProfile<ProductProfile>();
        cfg.AddProfile<ClothingProfile>();
        cfg.AddProfile<TagsProfile>();
      });
      _mapper = new Mapper(configuration);

      _fixture = new Fixture();
      _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
      _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

      _tested = new ProductsController(_loggerMock.Object,
                                       _mapper,
                                       _mediatorMock.Object);
    }

    [Fact]
    public async Task GivenGetProducts_WhenGetProductsIsSuccess_ThenReturnListOfProducts()
    {
      var expectedMediatorResults = _fixture.CreateMany<Product>();
      foreach (var expectedResult in expectedMediatorResults)
      {
        expectedResult.IdClothingNavigation.Id = expectedResult.IdClothing;
      }
      
      _mediatorMock
        .Setup(x => x.Send(It.IsAny<GetProducts>(), _cancellationToken))
        .ReturnsAsync(expectedMediatorResults);

      var result = await _tested.GetProducts();

      var okResult = Assert.IsType<OkObjectResult>(result);
      var response = Assert.IsAssignableFrom<IEnumerable<ProductResponse>>(okResult.Value);

      Assert.Equal(expectedMediatorResults.Select(x => x.IdClothing), response.Select(x => x.Id));

      _mediatorMock
        .Verify(x => x.Send(It.IsAny<GetProducts>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenGetProducts_WhenGetProductCatchProductException_ThenReturnProductError()
    {
      var expectedMediatorResult = _fixture.Create<ProductException>();

      _mediatorMock
        .Setup(x => x.Send(It.IsAny<GetProducts>(), _cancellationToken))
        .ThrowsAsync(expectedMediatorResult);

      var result = await _tested.GetProducts();

      var objectResult = Assert.IsType<ObjectResult>(result);

      var response = Assert.IsAssignableFrom<ProductError>(objectResult.Value);

      Assert.Equal(expectedMediatorResult.ErrorCode, response.ErrorCode);

      _mediatorMock
        .Verify(x => x.Send(It.IsAny<GetProducts>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenGetProductByCategory_WhenGetProductByCategoryIsSuccess_ThenReturnListOfProducts()
    {
      var category = "apparel";
      var expectedMediatorResults = _fixture.CreateMany<Product>(3);
      foreach (var expectedResult in expectedMediatorResults)
      {
        expectedResult.IdClothingNavigation.Id = expectedResult.IdClothing;
        expectedResult.IdClothingNavigation.Category = category;
      }

      _mediatorMock
        .Setup(x => x.Send(It.IsAny<GetProductByCategory>(), _cancellationToken))
        .ReturnsAsync(expectedMediatorResults);

      var result = await _tested.GetProductByCategory(category);

      var okResult = Assert.IsType<OkObjectResult>(result);
      var response = Assert.IsAssignableFrom<IEnumerable<ProductResponse>>(okResult.Value);

      Assert.Equal(expectedMediatorResults.Select(x => x.IdClothingNavigation.Category), response.Select(x => x.Category));

      _mediatorMock
        .Verify(x => x.Send(It.IsAny<GetProductByCategory>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenGetProductsByCategory_WhenGetProductByCategoryCatchProductException_ThenReturnProductError()
    {
      var category = "apparel";
      var expectedMediatorResult = _fixture.Create<ProductException>();

      _mediatorMock
        .Setup(x => x.Send(It.IsAny<GetProductByCategory>(), _cancellationToken))
        .ThrowsAsync(expectedMediatorResult);

      var result = await _tested.GetProductByCategory(category);

      var objectResult = Assert.IsType<ObjectResult>(result);

      var response = Assert.IsAssignableFrom<ProductError>(objectResult.Value);

      Assert.Equal(expectedMediatorResult.ErrorCode, response.ErrorCode);

      _mediatorMock
        .Verify(x => x.Send(It.IsAny<GetProductByCategory>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GivenAddProduct_WhenAddProductIsSucces_ThenReturnProduct()
    {
      var productRequest = _fixture.Create<ProductRequest>();
      productRequest.Name = "Hugo Boss";

      var expectedMediatorResult = _fixture.Create<Product>();
      expectedMediatorResult.IdClothingNavigation.Name = productRequest.Name; 

      _mediatorMock
        .Setup(x => x.Send(It.IsAny<AddProduct>(), _cancellationToken))
        .ReturnsAsync(expectedMediatorResult);

      var result = await _tested.AddProduct(productRequest);

      var okResult = Assert.IsType<OkObjectResult>(result);
      var response = Assert.IsAssignableFrom<ProductResponse>(okResult.Value);

      Assert.Equal(expectedMediatorResult.IdClothingNavigation.Name, response.Name);

      _mediatorMock
        .Verify(x => x.Send(It.IsAny<AddProduct>(), _cancellationToken), Times.Once);
    }


    [Fact]
    public async Task GivenAddProduct_WhenAddProductCatchProductException_ThenReturnProductError()
    {
      var productRequest = _fixture.Create<ProductRequest>();
      productRequest.Name = "Hugo Boss";

      var expectedMediatorResult = _fixture.Create<ProductException>();

      _mediatorMock
        .Setup(x => x.Send(It.IsAny<AddProduct>(), _cancellationToken))
        .ThrowsAsync(expectedMediatorResult);

      var result = await _tested.AddProduct(productRequest);

      var objectResult = Assert.IsType<ObjectResult>(result);

      var response = Assert.IsAssignableFrom<ProductError>(objectResult.Value);

      Assert.Equal(expectedMediatorResult.ErrorCode, response.ErrorCode);

      _mediatorMock
        .Verify(x => x.Send(It.IsAny<AddProduct>(), _cancellationToken), Times.Once);
    }
  }
}

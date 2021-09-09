using AutoMapper;
using CentricSoftwareWebApi.Application.Commands.Products.AddProduct;
using CentricSoftwareWebApi.Application.Queries.Products.GetProducts;
using CentricSoftwareWebApi.Application.Queries.Products.GetProductsById;
using CentricSoftwareWebApi.Domain.Exceptions;
using CentricSoftwareWebApi.Domain.Models;
using CentricSoftwareWebApi.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly ILogger<ProductsController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductsController(
      ILogger<ProductsController> logger, 
      IMapper mapper, 
      IMediator mediator)
    {
      _logger = logger;
      _mapper = mapper;
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
      try
      {
        _logger.LogInformation("Getting products");
        var products = await _mediator.Send(new GetProducts());
        return Ok(_mapper.Map<IEnumerable<ProductResponse>>(products));
      }
      catch (ProductException ex)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, new ProductError
        {
          ErrorCode = ex.ErrorCode,
          Message = ex.Message
        });
      }
    }
    [HttpGet("search")]
    public async Task<IActionResult> GetProductByCategory(
      [FromQuery(Name = "category")]
      [Required(ErrorMessage = "The field category is required")]
      string catgory)
    {
      try
      {
        _logger.LogInformation("Getting product by category");
        var product = await _mediator.Send(new GetProductByCategory(catgory));
        return Ok(_mapper.Map<IEnumerable<ProductResponse>>(product));
      }
      catch (ProductException ex)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, new ProductError 
        {
          ErrorCode = ex.ErrorCode,
          Message = ex.Message
        });
      }
    }
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductRequest productRequest)
    {
      try
      {
        _logger.LogInformation("Inserting product");
        var product = await _mediator.Send(new AddProduct(_mapper.Map<Product>(productRequest)));
        return Ok(_mapper.Map<ProductResponse>(product));
      }
      catch (ProductException ex)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, new ProductError
        {
          ErrorCode = ex.ErrorCode,
          Message = ex.Message
        });
      }
    }
  }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    // private readonly IProductRepository _repo;
    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IMapper _mapper;

    public ProductsController(IGenericRepository<Product> productsRepo,
                              IGenericRepository<ProductType> productTypeRepo,
                              IGenericRepository<ProductBrand> productBrandRepo,
                              IMapper mapper)
                              
    {
      _productBrandRepo = productBrandRepo;
      _mapper = mapper;
      _productTypeRepo = productTypeRepo;
      _productsRepo = productsRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
      // var products = await _context.Products.ToListAsync();
      var spec = new ProductsWithTypesAndBrandsSpec();
      var products = await _productsRepo.ListAsync(spec);

      // return products.Select(product => new ProductToReturnDto
      // {
      //    Id = product.Id,
      //   Name = product.Name,
      //   Description = product.Description,
      //   PictureUrl = product.PictureUrl,
      //   Price = product.Price,
      //   ProductBrand = product.ProductBrand.Name,
      //   ProductType = product.ProductType.Name
      // }).ToList();
      return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")] // How you specify that the route expects an id
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpec(id);
      // Get the product from the database
      var product =  await _productsRepo.GetEntityWithSpec(spec);
      // Return the Product DTO which will manage what fields will be sent
      // return new ProductToReturnDto
      // {
      //   Id = product.Id,
      //   Name = product.Name,
      //   Description = product.Description,
      //   PictureUrl = product.PictureUrl,
      //   Price = product.Price,
      //   ProductBrand = product.ProductBrand.Name,
      //   ProductType = product.ProductType.Name
      // };
      return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _productsRepo.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
      return Ok(await _productsRepo.ListAllAsync());
    }

  }
}
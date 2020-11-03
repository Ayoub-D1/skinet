using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductsWithTypesAndBrandsSpec : BaseSpecification<Product>
  {
    public ProductsWithTypesAndBrandsSpec()
    {
      AddInclude(x => x.ProductBrand);
      AddInclude(x => x.ProductType);
    }

    public ProductsWithTypesAndBrandsSpec(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.ProductBrand);
      AddInclude(x => x.ProductType);
    }
  }
}
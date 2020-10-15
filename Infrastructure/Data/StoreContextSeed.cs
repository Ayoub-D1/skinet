using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
  public class StoreContextSeed
  {
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
      try
      {
        if (!context.ProductBrands.Any())
        {
          // Since the SeedAsync method will be run from out Program.cs file in startup
          // which is within the API folder. We need to set the path from the API folder.
          var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
          var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
          foreach (var item in brands)
          {
            context.ProductBrands.Add(item);
          }
          await context.SaveChangesAsync();
        }
        if (!context.ProductTypes.Any())
        {
          // Since the SeedAsync method will be run from out Program.cs file in startup
          // which is within the API folder. We need to set the path from the API folder.
          var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
          var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
          foreach (var item in types)
          {
            context.ProductTypes.Add(item);
          }
          await context.SaveChangesAsync();
        }
        if (!context.Products.Any())
        {
          // Since the SeedAsync method will be run from out Program.cs file in startup
          // which is within the API folder. We need to set the path from the API folder.
          var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
          var products = JsonSerializer.Deserialize<List<Product>>(productsData);
          foreach (var item in products)
          {
            context.Products.Add(item);
          }
          await context.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogError(ex.Message);
      }
    }
  }
}
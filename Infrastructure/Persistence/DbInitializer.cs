using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task InitializerAsync()
        {
            try
            {
                //Create Database if dosen't exist & appling any Pending Migrations
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();
                //App Data Seeding
                if (!_storeContext.ProductTypes.Any())
                {
                    //read types from files
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\\Persistence\\Data\\Seeding\\types.json");

                    //Transform into C# objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    //Add To Db  & save Change
                    if (types != null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();

                    }
                }
                if (!_storeContext.ProductBrands.Any())
                {
                    //read types from files
                    var typesBrand = await File.ReadAllTextAsync(@"..\Infrastructure\\Persistence\\Data\\Seeding\\brands.json");

                    //Transform into C# objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(typesBrand);

                    //Add To Db  & save Change
                    if (brands != null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();

                    }
                }
                if (!_storeContext.Products.Any())
                {
                    //read types from files
                    var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    //Transform into C# objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    //Add To Db  & save Change
                    if (products != null && products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(products);
                        await _storeContext.SaveChangesAsync();

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
  

    }
}


using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> productBrandCollection)
        {
            bool existProductBrand = productBrandCollection.Find(p => true).Any();
            string path = "../Catalog.Infrastructure/Data/SeedData/brands.json";
            if (!existProductBrand)
            {
                var brandsData = File.ReadAllText(path);
                var brands = System.Text.Json.JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    productBrandCollection.InsertManyAsync(brands);
                }
            }
        }
    }
}

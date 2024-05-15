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
        public static void SeedData(IMongoCollection<Brands> productBrandCollection)
        {
            bool check = productBrandCollection.Find(p => true).Any();
            //string path1 = "../Services/Catalog.Infrastructure/Data/SeedData/brands.json"; //TODO
            string path = Path.Combine("Data", "SeedData", "brands.json");

            if (!check)
            {
                var brandsData = File.ReadAllText(path);
                var brands = System.Text.Json.JsonSerializer.Deserialize<List<Brands>>(brandsData);
                if (brands != null)
                {
                    productBrandCollection.InsertManyAsync(brands);
                }
            }
        }
    }
}

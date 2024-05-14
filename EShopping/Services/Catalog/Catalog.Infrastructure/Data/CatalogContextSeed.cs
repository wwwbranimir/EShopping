using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productsCollection)
        {
            bool exists = productsCollection.Find(p => true).Any();
            string path = "../Catalog.Infrastructure/Data/SeedData/products.json";
            if (!exists)
            {
                var data  = File.ReadAllText(path);
                var types = System.Text.Json.JsonSerializer.Deserialize<List<Product>>(data);
                if (types != null)
                {
                    productsCollection.InsertManyAsync(types);
                }
            }
        }
    }
}

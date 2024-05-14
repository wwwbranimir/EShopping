using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typesCollection)
        {
            bool existProductBrand = typesCollection.Find(p => true).Any();
            string path = Path.Combine("Data", "SeedData", "types.json");
            if (!existProductBrand)
            {
                var typesData = File.ReadAllText(path);
                var types = System.Text.Json.JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    typesCollection.InsertManyAsync(types);
                }
            }
        }
    }
}

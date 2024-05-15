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
        public static void SeedData(IMongoCollection<Types> typesCollection)
        {
            bool check = typesCollection.Find(p => true).Any();
           
            if (!check)
            {
                // string path = "../Catalog.Infrastructure/Data/SeedData/types.json";//TODO
                string path = Path.Combine("Data", "SeedData", "types.json");
                var typesData = File.ReadAllText(path);
                var types = System.Text.Json.JsonSerializer.Deserialize<List<Types>>(typesData);
                if (types != null)
                {
                    typesCollection.InsertManyAsync(types);
                }
            }
        }
    }
}

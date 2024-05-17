using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Data", "SeedData", "types.json");
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

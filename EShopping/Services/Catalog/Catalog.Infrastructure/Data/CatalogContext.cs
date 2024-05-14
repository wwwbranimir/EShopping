using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        //getters for the Products, Brands, and Types collections
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<ProductBrand> Brands { get; }
        public IMongoCollection<ProductType> Types { get; }

        public CatalogContext(IConfiguration settings)
        {
           
            var client = new MongoClient(settings.GetValue<string>("DatabaseSettings:ConnectionString"));
          
            var database = client.GetDatabase(settings.GetValue<string>("DatabaseSettings:DatabaseName"));
          
            Products = database.GetCollection<Product>(settings.GetValue<string>("DatabaseSettings:CollectionName"));

            Brands = database.GetCollection<ProductBrand>(settings.GetValue<string>("DatabaseSettings:BrandsCollection"));
            Types = database.GetCollection<ProductType>(settings.GetValue<string>("DatabaseSettings:TypesCollection"));

          
            //call the SeedData method to seed the database with initial data
            BrandContextSeed.SeedData(Brands);
            TypeContextSeed.SeedData(Types);
            CatalogContextSeed.SeedData(Products);
           
            
        }


        
    }
}

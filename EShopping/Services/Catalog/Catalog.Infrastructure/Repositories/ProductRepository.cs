using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext context)
        {
            this._catalogContext = context;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
            return product;


        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;


        }

        public async Task<IEnumerable<Brands>> GetAllBrands()
        {
            return await _catalogContext.Brands.Find(p => true).ToListAsync();


        }

        public async Task<IEnumerable<Types>> GetAllTypes()
        {
            return await _catalogContext.Types.Find(p => true).ToListAsync();

        }

        public async Task<Product> GetProductById(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);
            return await _catalogContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _catalogContext.Products.Find(filter).ToListAsync();


        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecificationParameters parameters)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(parameters.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(parameters.Search));
                filter &= searchFilter;
            }
            if (!string.IsNullOrEmpty(parameters.BrandId))
            {
                var brandFilter = builder.Eq(x => x.Brands.Id, parameters.BrandId);
                filter &= brandFilter;
            }
            if (!string.IsNullOrEmpty(parameters.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Types.Id, parameters.TypeId);
                filter &= typeFilter;
            }
            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                return new Pagination<Product>()
                {
                    PageIndex = parameters.PageIndex,
                    PageSize = parameters.PageSize,
                    Data = await DataFilter(parameters, filter),
                    Count = await _catalogContext.Products.CountDocumentsAsync(p => true)
                };
            }
            return new Pagination<Product>()
            {
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                Data = await _catalogContext.Products.
                        Find(filter).
                        Sort(Builders<Product>.Sort.Ascending(p=>p.Name))
                        .Skip((parameters.PageIndex - 1) * parameters.PageSize)
                        .Limit(parameters.PageSize).ToListAsync(),
                Count = await _catalogContext.Products.CountDocumentsAsync(p => true)
            };

        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecificationParameters parameters, FilterDefinition<Product> filter) =>

            parameters.Sort switch
            {
                "priceAsc" => await _catalogContext.Products.Find(filter).Sort(Builders<Product>.Sort.Ascending("Price")).ToListAsync(),
                "priceDesc" => await _catalogContext.Products.Find(filter).Sort(Builders<Product>.Sort.Descending("Price")).ToListAsync(),
                _ => await _catalogContext.Products.Find(filter).Sort(Builders<Product>.Sort.Ascending("Name")).ToListAsync(),
            };

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }
    }
}

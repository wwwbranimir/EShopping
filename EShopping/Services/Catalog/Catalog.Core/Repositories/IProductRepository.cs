using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository 
    {
        Task<Pagination<Product>> GetProducts(CatalogSpecificationParameters parameters);
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByBrand(string name);
        //create product
        Task<Product> CreateProduct(Product product);
        //update product
        Task<bool> UpdateProduct(Product product);
        //delete product
        Task<bool> DeleteProduct(string id);
    }
}
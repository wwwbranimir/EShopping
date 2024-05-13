using Catalog.Core.Entities;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository 
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
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
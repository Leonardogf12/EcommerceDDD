using Entities.Entities;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericApp<Product>
    {
        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task<List<Product>> ListProductsUser(string idUser);

        Task<List<Product>> ListProductsWithStock(string description);

        Task<List<Product>> ListProductsUserCart(string idUser);

        Task<Product> GetProductCart(int idProductCart);
    }
}

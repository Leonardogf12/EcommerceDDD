using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduct : IGenerics<Product>
    {
        Task<List<Product>> ListProductsUser(string idUser);

        Task<List<Product>> ListProducts(Expression<Func<Product, bool>> exProdut);

        Task<List<Product>> ListProductsUserCart(string idUser);

        Task<Product> GetProductCart(int idProductCart);
    }
}

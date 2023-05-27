using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduct : IGenerics<Product>
    {
        Task<List<Product>> ListProductsUser(string idUser);
    }
}

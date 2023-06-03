using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//*INTERFACE DE PRODUTO COM REGRAS DE NEGOCIOS
namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        Task AddProduct(Product product);

        Task UpdateProduct(Product product);

        Task<List<Product>> ListProductsWithStock(string description);
    }
}

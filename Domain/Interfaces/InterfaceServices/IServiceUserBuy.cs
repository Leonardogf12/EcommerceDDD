using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//*INTERFACE DE COMPRASUSUARIO COM REGRAS DE NEGOCIOS
namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceUserBuy
    {
        Task<UserPurchase> ShoppingCart(string idUser);

        Task<UserPurchase> PurchasedProducts(string idUser);
    }
}

using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Enums;

//*INTERFACE DE COMPRASUSUARIO SEM REGRAS DE NEGOCIO.
namespace Domain.Interfaces.InterfaceUserBuy
{
    public interface IUserBuy : IGenerics<UserPurchase>
    {
        Task<int> QtyProductCartUser(string idUser);

        Task<UserPurchase> ProductsPurchaseByStatus(string idUser, EnumPurchaseStatus status);

        Task<bool> ConfirmPurchaseCartUser(string idUser);
    }
}

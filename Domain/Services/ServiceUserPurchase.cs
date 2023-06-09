﻿using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUserBuy;
using Entities.Entities;

//*CLASSE QUE HERDA DA INTERFACE IServiceUserBuy - COM REGRA DE NEGOCIOS(tem validacoes, where etc.)
namespace Domain.Services
{
    public class ServiceUserPurchase : IServiceUserBuy
    {
        private readonly IUserBuy _IUserBuy;

        public ServiceUserPurchase(IUserBuy iUserBuy)
        {
            _IUserBuy = iUserBuy;
        }

        public  async Task<UserPurchase> PurchasedProducts(string idUser)
        {
           return await _IUserBuy.ProductsPurchaseByStatus(idUser, Entities.Enums.EnumPurchaseStatus.Produto_Carrinho);
        }

        public async Task<UserPurchase> ShoppingCart(string idUser)
        {
            return await _IUserBuy.ProductsPurchaseByStatus(idUser, Entities.Enums.EnumPurchaseStatus.Produto_Comprado);
        }
    }
}

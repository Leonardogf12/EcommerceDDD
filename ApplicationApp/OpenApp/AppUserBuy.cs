using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUserBuy;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppUserBuy : InterfaceUserBuyApp
    {
        private readonly IUserBuy _IUserbuy;
        private readonly IServiceUserBuy _IServiceUserBuy;

        public AppUserBuy(IUserBuy IUserbuy, IServiceUserBuy IServiceUserBuy)
        {            
            _IUserbuy = IUserbuy;
            _IServiceUserBuy = IServiceUserBuy;
        }

        #region CONSULTAS CUSTOMIZADAS

        public async Task<int> QtyProductCartUser(string idUser)
        {
            return await _IUserbuy.QtyProductCartUser(idUser);
        }

        public async Task<UserPurchase> ShoppingCart(string idUser)
        {
            return await _IServiceUserBuy.ShoppingCart(idUser);
        }

        public async Task<UserPurchase> PurchasedProducts(string idUser)
        {
            return await _IServiceUserBuy.PurchasedProducts(idUser);
        }

        public async Task<bool> ConfirmPurchaseCartUser(string idUser)
        {
            return await _IUserbuy.ConfirmPurchaseCartUser(idUser);
        }

        #endregion

        #region CONSULTAS PADRAO

        public async Task Add(UserPurchase Object)
        {
            await _IUserbuy.Add(Object);
        }

        public async Task Delete(UserPurchase Object)
        {
            await _IUserbuy.Delete(Object);
        }

        public async Task<UserPurchase> GetEntityById(int Id)
        {
            return await _IUserbuy.GetEntityById(Id);
        }

        public async Task<List<UserPurchase>> List()
        {
            return await _IUserbuy.List();
        }

        public async Task Update(UserPurchase Object)
        {
            await _IUserbuy.Update(Object);
        }        

        #endregion
    }
}

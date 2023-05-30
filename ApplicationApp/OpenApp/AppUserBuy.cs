using ApplicationApp.Interfaces;
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

        public AppUserBuy(IUserBuy IUserbuy)
        {            
            _IUserbuy = IUserbuy;
        }


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
    }
}

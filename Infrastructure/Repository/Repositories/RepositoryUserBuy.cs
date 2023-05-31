using Domain.Interfaces.InterfaceUserBuy;
using Entities.Entities;
using Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryUserBuy : RepositoryGenerics<UserPurchase>, IUserBuy
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryUserBuy()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<int> QtyProductCartUser(string idUser)
        {
            using (var db = new ContextBase(_optionsBuilder))
            {
                return await db.UserPurchase.CountAsync(x=>x.UserId == idUser && x.EnumPurchaseStatus == EnumPurchaseStatus.Produto_Carrinho);
            }
        }
    }
}

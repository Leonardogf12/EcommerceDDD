using Domain.Interfaces.InterfaceUserBuy;
using Entities.Entities;
using Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

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

        public async Task<UserPurchase> ProductsPurchaseByStatus(string idUser, EnumPurchaseStatus status)
        {
            using (var db = new ContextBase(_optionsBuilder))
            {
                var purchaseUser = new UserPurchase();

                purchaseUser.ListProducts = new List<Product>();

                var cartProductsUser = await (from p in db.Product
                                              join c in db.UserPurchase on p.Id equals c.ProductId
                                              where c.UserId.Equals(idUser) && c.EnumPurchaseStatus == status
                                              select new Product
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Description = p.Description,
                                                  Observation = p.Observation,
                                                  Value = p.Value,
                                                  QtyBuy = c.QtyPurchase,
                                                  IdProductCart = c.Id,
                                                  Url = p.Url
                                              }).AsNoTracking().ToListAsync();

                purchaseUser.ListProducts = cartProductsUser;
                purchaseUser.User = await db.User.FirstOrDefaultAsync(x=>x.Id == idUser);
                purchaseUser.QtyProducts = cartProductsUser.Count();
                purchaseUser.AddressComplet = string.Concat(purchaseUser.User.Address, "-",
                                                            purchaseUser.User.ComplementAddress, " - CEP: ",
                                                            purchaseUser.User.Cep);
                purchaseUser.TotalValue = cartProductsUser.Sum(x => x.Value);
                purchaseUser.EnumPurchaseStatus = status;

                return purchaseUser;
            }
        }

        public async Task<bool> ConfirmPurchaseCartUser(string idUser)
        {
            try
            {
                using (var db = new ContextBase(_optionsBuilder))
                {
                    var purchaseUser = new UserPurchase();

                    purchaseUser.ListProducts = new List<Product>();

                    var cartProductsUser = await (from p in db.Product
                                                  join c in db.UserPurchase on p.Id equals c.ProductId
                                                  where c.UserId.Equals(idUser)
                                                  && c.EnumPurchaseStatus == EnumPurchaseStatus.Produto_Carrinho
                                                  select c).AsNoTracking().ToListAsync();

                    cartProductsUser.ForEach(p =>
                    {
                        p.EnumPurchaseStatus = EnumPurchaseStatus.Produto_Comprado;
                    });

                    db.UpdateRange(cartProductsUser);
                    await db.SaveChangesAsync();

                    return true;

                }
              
            }
            catch (Exception e)
            {
                return false;
            }
                        
        }
    }
}

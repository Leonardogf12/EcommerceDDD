using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Product>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryProduct()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }
        
        public async Task<List<Product>> ListProductsUser(string idUser)
        {
            using(var db = new ContextBase(_optionsBuilder))
            {
                return await db.Product.Where(x => x.UserId == idUser).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Product>> ListProducts(Expression<Func<Product, bool>> exProdut)
        {
            using (var db = new ContextBase(_optionsBuilder))
            {
                return await db.Product.Where(exProdut).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Product>> ListProductsUserCart(string idUser)
        {
            using (var db = new ContextBase(_optionsBuilder))
            {
                var productsUserCart = await (from pr in db.Product
                                              join up in db.UserPurchase on pr.Id equals up.ProductId
                                              where up.UserId.Equals(idUser)
                                              && up.EnumPurchaseStatus == EnumPurchaseStatus.Produto_Carrinho
                                              select new Product
                                              {
                                                  Id = pr.Id,
                                                  Name = pr.Name,
                                                  Description = pr.Description,
                                                  Observation = pr.Observation,
                                                  Value = pr.Value,
                                                  QtyBuy = up.QtyPurchase,
                                                  IdProductCart = up.Id,

                                              }).AsNoTracking().ToListAsync();

                return productsUserCart;
            }
        }

        public async Task<Product> GetProductCart(int idProductCart)
        {
            using (var db = new ContextBase(_optionsBuilder))
            {
                var productUserCart = await (from pr in db.Product
                                              join up in db.UserPurchase on pr.Id equals up.ProductId
                                              where up.Id.Equals(idProductCart)
                                              && up.EnumPurchaseStatus == EnumPurchaseStatus.Produto_Carrinho
                                              select new Product
                                              {
                                                  Id = pr.Id,
                                                  Name = pr.Name,
                                                  Description = pr.Description,
                                                  Observation = pr.Observation,
                                                  Value = pr.Value,
                                                  QtyBuy = up.QtyPurchase,
                                                  IdProductCart = up.Id,

                                              }).AsNoTracking().FirstOrDefaultAsync();

                return productUserCart;
            }
        }
    }
}

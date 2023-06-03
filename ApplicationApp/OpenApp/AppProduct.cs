using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppProduct : InterfaceProductApp
    {
        IProduct _IProduct;
        IServiceProduct _IServiceProduct;        

        public AppProduct(IProduct IProduct, IServiceProduct IServiceProduct)
        {
            _IProduct = IProduct;
            _IServiceProduct = IServiceProduct;
        }

        #region CONSULTAS CUSTOMIZADAS

        public async Task AddProduct(Product product)
        {
            await _IServiceProduct.AddProduct(product);
        }

        public async Task UpdateProduct(Product product)
        {
            await _IServiceProduct.UpdateProduct(product);
        }

        public async Task<List<Product>> ListProductsUser(string idUser)
        {
            return await _IProduct.ListProductsUser(idUser);
        }

        public async Task<List<Product>> ListProductsWithStock(string description)
        {
            return await _IServiceProduct.ListProductsWithStock(description);
        }

        public async Task<List<Product>> ListProductsUserCart(string idUser)
        {
           return await _IProduct.ListProductsUserCart(idUser);
        }

        public async Task<Product> GetProductCart(int idProductCart)
        {
            return await _IProduct.GetProductCart(idProductCart);
        }

        #endregion

        #region CONSULTAS PADRAO

        public async Task Add(Product product)
        {
            await _IProduct.Add(product);
        }
       
        public async Task Delete(Product product)
        {
            await _IProduct.Delete(product);
        }

        public async Task<Product> GetEntityById(int Id)
        {
            return await _IProduct.GetEntityById(Id);          
        }

        public async Task<List<Product>> List()
        {
            return await _IProduct.List();
        }

        public async Task Update(Product product)
        {
            await _IProduct.Update(product);
        }        

        #endregion
    }
}

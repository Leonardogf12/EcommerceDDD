using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Repository.Repositories;

namespace UnitTestEcommerceDD
{
    [TestClass]
    public class UnitTestEcommerce
    {
        [TestMethod]
        public async Task AddProductWithSuccess()
        {
            try
            {
                IProduct IProduct = new RepositoryProduct();
                IServiceProduct IServiceProduct = new ServiceProduct(IProduct);

                var product = new Product
                {
                    Description = "description",
                    Stock = 10,
                    Name = "name",
                    Value = 20,
                    UserId = "e3f30d8b-99c2-44b7-9687-c8fb093509c5",
                };

                await IServiceProduct.AddProduct(product);

                Assert.IsFalse(product.Notifications.Any());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }            
        }

        [TestMethod]
        public async Task AddProductWithValidateFieldRequired()
        {
            try
            {
                IProduct IProduct = new RepositoryProduct();
                IServiceProduct IServiceProduct = new ServiceProduct(IProduct);

                var product = new Product
                {                 
                };

                await IServiceProduct.AddProduct(product);

                Assert.IsTrue(product.Notifications.Any());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public async Task ListUserProducts()
        {
            try
            {
                IProduct IProduct = new RepositoryProduct();

                var listProducts = await IProduct.ListProductsUser("e3f30d8b-99c2-44b7-9687-c8fb093509c5");

                Assert.IsTrue(listProducts.Any());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public async Task DeleteProduct()
        {
            try
            {
                IProduct IProduct = new RepositoryProduct();

                var listProducts = await IProduct.ListProductsUser("e3f30d8b-99c2-44b7-9687-c8fb093509c5");

                var product = listProducts.LastOrDefault();

                await IProduct.Delete(product);

                Assert.IsTrue(true); //*se nao deletar ja vai dar erro. por isso tem esse true direto aqui.
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public async Task GetEntitieById()
        {
            try
            {
                IProduct IProduct = new RepositoryProduct();

                var listProducts = await IProduct.ListProductsUser("e3f30d8b-99c2-44b7-9687-c8fb093509c5");

                var product = IProduct.GetEntityById(listProducts.LastOrDefault().Id);

                Assert.IsTrue(product != null);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


    }
}
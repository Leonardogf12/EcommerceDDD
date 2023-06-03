using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

//*CLASSE QUE HERDA DA INTERFACE IServiceProduct - COM REGRA DE NEGOCIOS(tem validacoes, where etc.)
namespace Domain.Services
{
    public class ServiceProduct : IServiceProduct
    {
        private readonly IProduct _product;

        public ServiceProduct(IProduct product)
        {
            _product = product;
        }

        public async Task AddProduct(Product product)
        {
            var validateName = product.ValidatePropertyString(product.Name, "Name");

            var validateValue = product.ValidatePropertyDecimal(product.Value, "Value");

            var validateQtyStock = product.ValidatePropertyInt(product.Stock, "Stock");

            if (validateName && validateValue && validateQtyStock)
            {
                product.DateRegister = DateTime.Now;
                product.DateChange = DateTime.Now;

                product.Status = true;
                await _product.Add(product);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            var validateName = product.ValidatePropertyString(product.Name, "Name");

            var validateValue = product.ValidatePropertyDecimal(product.Value, "Value");

            var validateQtyStock = product.ValidatePropertyInt(product.Stock, "Stock");

            if (validateName && validateValue && validateQtyStock)
            {
                product.DateChange = DateTime.Now;
                await _product.Update(product);
            }
        }

        public async Task<List<Product>> ListProductsWithStock(string description)
        {

            if (string.IsNullOrWhiteSpace(description))
            {
                return await _product.ListProducts(x => x.Stock > 0);
            }
            else
            {
                return await _product.ListProducts(x => x.Stock > 0
                                                    && x.Name.ToUpper().Contains(description.ToUpper()));
            }

        }
    }
}

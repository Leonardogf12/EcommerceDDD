using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{

    [Authorize]
    public class ProductsController : Controller
    {
        public readonly UserManager<User> _userManager;

        public readonly InterfaceProductApp _InterfaceProductApp;

        public ProductsController(InterfaceProductApp interfaceProductApp, UserManager<User> userManager)
        {
            _InterfaceProductApp = interfaceProductApp;
            _userManager = userManager;            
        }

        #region GETS

        public async Task<IActionResult> Index()
        {
            var idUser = await GetIdUserLogged();

            return View(await _InterfaceProductApp.ListProductsUser(idUser));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        public async Task<IActionResult> Create()
        {
            //*IMPLEMENTAR
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        #endregion

        #region POSTS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {

                product.UserId = await GetIdUserLogged();
                
                await _InterfaceProductApp.AddProduct(product);

                if (product.Notifications.Any())
                {
                    foreach (var erro in product.Notifications)
                    {
                        ModelState.AddModelError(erro.NameProperty, erro.Message);
                    }

                    return View("Create", product);
                }
            }
            catch (Exception e)
            {
                return View("Create", product);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            try
            {
                await _InterfaceProductApp.UpdateProduct(product);

                if (product.Notifications.Any())
                {
                    foreach (var erro in product.Notifications)
                    {
                        ModelState.AddModelError(erro.NameProperty, erro.Message);
                    }

                    return View("Edit", product);
                }
            }
            catch (Exception e)
            {
                return View("Edit", product);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Product product)
        {
            try
            {
                var productDel = await _InterfaceProductApp.GetEntityById(id);
                await _InterfaceProductApp.Delete(productDel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        #endregion

        #region COMUM

        private async Task<string> GetIdUserLogged()
        {
            var idUser = await _userManager.GetUserAsync(User);

            return idUser != null ? idUser.Id : string.Empty;
        }

        #endregion

        #region APIS

        [AllowAnonymous]
        [HttpGet("api/ListProductsWithStock")]
        public async Task<JsonResult> ListProductsWithStock()
        {
            return Json(await _InterfaceProductApp.ListProductsWithStock());
        }

        [AllowAnonymous]
        [HttpPost("api/AddProductCart")]
        public async Task AddProductCart(string id, string name, string qtd)
        {
           ////
        }

        #endregion
    }
}
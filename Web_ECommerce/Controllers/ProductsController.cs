using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{

    [Authorize]
    public class ProductsController : Controller
    {
        public readonly InterfaceProductApp _InterfaceProductApp;

        public ProductsController(InterfaceProductApp interfaceProductApp)
        {
            _InterfaceProductApp = interfaceProductApp;
            
        }

        #region GETS

        public async Task<IActionResult> Index()
        {
            return View(await _InterfaceProductApp.List());
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
                await _InterfaceProductApp.AddProduct(product);

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
    }
}
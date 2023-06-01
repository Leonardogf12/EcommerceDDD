using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Security;
using System.Security.Permissions;

namespace Web_ECommerce.Controllers
{

    [Authorize]
    public class ProductsController : Controller
    {
        public readonly UserManager<User> _userManager;

        public readonly InterfaceProductApp _InterfaceProductApp;

        public readonly InterfaceUserBuyApp _InterfaceUserBuyApp;

        private IWebHostEnvironment _webHostEnvironment;

        public ProductsController(InterfaceProductApp interfaceProductApp,
            InterfaceUserBuyApp interfaceUserBuyApp,
            IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager)
        {
            _InterfaceProductApp = interfaceProductApp;
            _InterfaceUserBuyApp = interfaceUserBuyApp;
            _webHostEnvironment = webHostEnvironment;
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

                await SaveImageProduct(product);
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

                    ViewBag.Alert = true;
                    ViewBag.Message = "Verifique, algo deu errado.";


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task SaveImageProduct(Product product)
        {
            //var productView = await _InterfaceProductApp.GetEntityById(product.Id);

            try
            {
                if (product.Image != null)
                {
                    var webRoot = _webHostEnvironment.WebRootPath;
                    var permissionSet = new PermissionSet(PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append,
                        string.Concat(webRoot, "/imgProducts"));
                    permissionSet.AddPermission(writePermission);

                    var extension = Path.GetExtension(product.Image.FileName);
                    var nameFile = string.Concat(product.Id.ToString(), extension);
                    var pathFileSave = string.Concat(webRoot, "\\imgProducts\\", nameFile);

                    product.Image.CopyTo(new FileStream(pathFileSave, FileMode.Create));

                    product.Url = string.Concat("https://localhost:7279", "/imgProducts/", nameFile);

                    await _InterfaceProductApp.UpdateProduct(product);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        #endregion

        #region COMUM

        private async Task<string> GetIdUserLogged()
        {
            var idUser = await _userManager.GetUserAsync(User);

            return idUser != null ? idUser.Id : string.Empty;
        }

        public async Task<IActionResult> ListProductsCart()
        {
            var idUser = await _userManager.GetUserAsync(User);

            return View(await _InterfaceProductApp.ListProductsUserCart(idUser.Id));
        }

        public async Task<IActionResult> RemoveProductCart(int id)
        {
            return View(await _InterfaceProductApp.GetProductCart(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProductCart(int id, Product product)
        {
            try
            {
                var productDel = await _InterfaceUserBuyApp.GetEntityById(id);
                await _InterfaceUserBuyApp.Delete(productDel);

                return RedirectToAction(nameof(ListProductsCart));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        #endregion

        #region APIS

        [AllowAnonymous]
        [HttpGet("api/ListProductsWithStock")]
        public async Task<JsonResult> ListProductsWithStock()
        {
            return Json(await _InterfaceProductApp.ListProductsWithStock());
        }

        #endregion

        
    }
}
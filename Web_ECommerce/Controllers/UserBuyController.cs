using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{
    public class UserBuyController : Controller
    {
        public readonly UserManager<User> _userManager;

        public readonly InterfaceUserBuyApp _InterfaceUserBuyApp;

        public UserBuyController(InterfaceUserBuyApp InterfaceUserBuyApp, 
                                  UserManager<User> userManager)
        {
           _InterfaceUserBuyApp = InterfaceUserBuyApp;
            _userManager = userManager;
        }

        #region GETS

        public async Task<IActionResult> FinishBuy()
        {
            var userLogged = await _userManager.GetUserAsync(User);

            var userPurchase = await _InterfaceUserBuyApp.PurchasedProducts(userLogged.Id);

            return View(userPurchase);
        }

        public async Task<IActionResult> MyShopping(bool message = false)
        {
            var userLogged = await _userManager.GetUserAsync(User);
            var userBuy = await _InterfaceUserBuyApp.PurchasedProducts(userLogged.Id);

            if (message)
            {
                ViewBag.Success = true;
                ViewBag.Message = "Compra efetivada com sucesso. Pague o boleto para garantir sua compra!.";
            }

            return View(userBuy);
        }

        public async Task<IActionResult> ConfirmUserPurchase()
        {
            var userLogged = await _userManager.GetUserAsync(User);

            var result = await _InterfaceUserBuyApp.ConfirmPurchaseCartUser(userLogged.Id);

            if (result)            
                return RedirectToAction("MyShopping", new { message = true });
            else
                return RedirectToAction("FinishBuy");
        }

        #endregion

        #region APIS

        [AllowAnonymous]
        [HttpPost("api/AddProductCart")]
        public async Task<JsonResult> AddProductCart(string id, string name, string qty)
        {
            var userLogged = await _userManager.GetUserAsync(User);

            if (userLogged != null)
            {
                await _InterfaceUserBuyApp.Add(new UserPurchase
                {
                    ProductId = Convert.ToInt32(id),
                    QtyPurchase = Convert.ToInt32(qty),
                    EnumPurchaseStatus = EnumPurchaseStatus.Produto_Carrinho,
                    UserId = userLogged.Id
                });

                return Json(new { success = true });
            }

            return Json(new { success = false});
        }
       
        [HttpGet("api/GetQtyProductsUserCart")]
        public async Task<JsonResult> GetQtyProductsUserCart()
        {
            var userLogged = await _userManager.GetUserAsync(User);
            var qty = 0;

            if (userLogged != null)
            {
                qty = await _InterfaceUserBuyApp.QtyProductCartUser(userLogged.Id);               
            
            return Json(new { success = true, qtd = qty });
            }   
            return Json(new { success = false, qtd = qty });
        }

        #endregion

    }
}

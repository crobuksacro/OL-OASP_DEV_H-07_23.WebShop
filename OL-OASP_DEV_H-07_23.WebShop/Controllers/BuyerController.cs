using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop.Controllers
{
    [Authorize(Roles = Roles.Buyer)]
    public class BuyerController : Controller
    {
        private readonly IBuyerService buyerService;
        private readonly IProductService productService;


        public BuyerController(IBuyerService buyerService, IProductService productService)
        {
            this.buyerService = buyerService;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categorys = await productService.GetProductCategories();
            return View(categorys);
        }


    }
}

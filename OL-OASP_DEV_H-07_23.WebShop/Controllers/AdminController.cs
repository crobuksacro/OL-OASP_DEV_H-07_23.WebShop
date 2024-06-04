using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.Buyer}")]
    public class AdminController : Controller
    {
        private readonly IProductService productService;

        public AdminController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categorys = await productService.GetProductCategories();
            return View(categorys);
        }


    }
}

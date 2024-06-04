using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
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


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryBinding model)
        {
            await productService.AddProductCategory(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            var model = await productService.GetProductCategory<ProductCategoryUpdateBinding>(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryUpdateBinding model)
        {
            await productService.UpdateProductCategory(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            await productService.DeleteProductCategory(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(long id)
        {
            var productCategory = await productService.GetProductCategory(id);
            return View(productCategory);
        }
    }
}

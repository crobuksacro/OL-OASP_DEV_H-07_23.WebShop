using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.Buyer}")]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IBuyerService buyerService;
        private readonly IAdminService adminService;

        public AdminController(IProductService productService, IBuyerService buyerService, IAdminService adminService)
        {
            this.productService = productService;
            this.buyerService = buyerService;
            this.adminService = adminService;
        }

        public async Task<IActionResult> Company()
        {
            var company = await adminService.GetCompany<CompanyUpdateBinding>();
            return View(company);
        }
        [HttpPost]
        public async Task<IActionResult> Company(CompanyUpdateBinding model)
        {
            var company = await adminService.UpdateCompany(model);
            return RedirectToAction(nameof(Company));
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await buyerService.GetOrders(User);
            return View(orders);
        }

        public async Task<IActionResult> Order(long id)
        {
            var order = await buyerService.GetOrder(id, User);
            return View(order);
        }

        public async Task<IActionResult> CancelOrder(long id)
        {
            var orders = await buyerService.CancelOrder(id);
            return RedirectToAction(nameof(Orders));
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
            return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await productService.DeleteProductCategory(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(long id)
        {
            var productCategory = await productService.GetProductCategory(id);
            return View(productCategory);
        }

        public async Task<IActionResult> AddProductItem(long categoryId)
        {
            var model = new ProductItemBinding
            {
                ProductCategoryId = categoryId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductItem(ProductItemBinding model)
        {

            await productService.AddProductItem(model);

            return RedirectToAction(nameof(Details), new { id = model.ProductCategoryId });
        }

        public async Task<IActionResult> DeleteProductItem(long id)
        {
            var response = await productService.DeleteProductItem(id);
            return RedirectToAction(nameof(Details), new { id = response.ProductCategoryId });
        }
    }
}

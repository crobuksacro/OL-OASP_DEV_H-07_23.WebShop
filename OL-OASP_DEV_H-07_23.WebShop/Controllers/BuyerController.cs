using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using System.Text.Json;

namespace OL_OASP_DEV_H_07_23.WebShop.Controllers
{
    [Authorize(Roles = Roles.Buyer)]
    public class BuyerController : Controller
    {
        private readonly IBuyerService buyerService;
        private readonly IProductService productService;
        private readonly IAccountService accountService;

        public BuyerController(IBuyerService buyerService, IProductService productService, IAccountService accountService)
        {
            this.buyerService = buyerService;
            this.productService = productService;
            this.accountService = accountService;
        }
        public async Task<IActionResult> Order()
        {
            var sessionOrderItems = HttpContext.Session.GetString("OrderItems");
            List<OrderItemBinding> existingOrderItems = sessionOrderItems != null ?
                JsonSerializer.Deserialize<List<OrderItemBinding>>(sessionOrderItems) : new List<OrderItemBinding>();

            var response = new OrderBinding
            {
                OrderItems = existingOrderItems,
                OrderAddress = await accountService.GetUserAddress<AddressBinding>(User)
            };

            return View(response);
        }

        public async Task<IActionResult> MyOrders()
        {
            var orders = await buyerService.GetOrders(User);
            return View(orders);
        }

        public async Task<IActionResult> MyOrder(long id)
        {
            var orders = await buyerService.GetOrder(id);
            return View(orders);
        }


        [HttpPost]
        public async Task<IActionResult> Order(OrderBinding model)
        {
           var order = await buyerService.AddOrder(model,User);
           HttpContext.Session.Remove("OrderItems");

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Index()
        {
            var categorys = await productService.GetProductCategories();
            return View(categorys);
        }

        public async Task<IActionResult> Details(long id)
        {
            var category = await productService.GetProductCategory(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddToOrderItem([FromBody] List<OrderItemBinding> orderItems)
        {
            try
            {
                // Retrieve the existing session list of OrderItemBiding
                var sessionOrderItems = HttpContext.Session.GetString("OrderItems");

                List<OrderItemBinding> existingOrderItems = sessionOrderItems != null ?
                    JsonSerializer.Deserialize<List<OrderItemBinding>>(sessionOrderItems) : new List<OrderItemBinding>();

                foreach (var orderItem in orderItems)
                {
                    var existingOrderItem = existingOrderItems.FirstOrDefault(item => item.ProductItemId == orderItem.ProductItemId);

                    if (existingOrderItem != null)
                    {
                        existingOrderItem.Quantity += orderItem.Quantity;
                    }
                    else
                    {
                        existingOrderItems.Add(orderItem);
                    }

                }

                HttpContext.Session.SetString("OrderItems", JsonSerializer.Serialize(existingOrderItems));

                return Json(existingOrderItems);
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }

        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Moq;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Mapping;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop.UnitTest
{
    public abstract class WebShopSetup
    {
        protected IMapper Mapper { get; private set; }
        protected ApplicationDbContext InMemoryDbContext;
        protected readonly Mock<IOptions<AppSettings>> AppSettings;
        protected static string TestString = "mali medo";
        protected readonly List<ProductCategory> ProductCategories;
        protected readonly List<Order> Orders;
        protected readonly ApplicationUser ApplicationUser;
        protected readonly Mock<UserManager<ApplicationUser>> UserManager;


        public WebShopSetup()
        {
            SetupInMemoryContext();

            var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();
            UserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);
            ApplicationUser = GetApplicationUser();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            Mapper = mappingConfig.CreateMapper();
            var config = new AppSettings
            {
                PaginationOffset = 10
            };
            AppSettings = new Mock<IOptions<AppSettings>>();
            AppSettings.Setup(c => c.Value).Returns(config);
            ProductCategories = GenerateProductCategorys(100);
            Orders = GetOrders(1);

        }


        protected ApplicationUser GetApplicationUser()
        {
            var applicationUser = new ApplicationUser
            {
                UserName = "test",
                Email = "test@test.hr",
                Address = new Address
                {
                    City = "Zagreb",
                    Country = "Croatia",
                    Street = "Ilica 1",
                    Number = "10a"
                },
                LastName = "test",
                FirstName = "test",
                PhoneNumber = "123456789",
                EmailConfirmed = true

            };

            InMemoryDbContext.Users.Add(applicationUser);
            InMemoryDbContext.SaveChanges();
            return applicationUser;
        }

        protected List<Order> GetOrders(int number)
        {
            List<Order> orders = new List<Order>();

            for (int i = 0; i < number; i++)
            {
                var order = new Order
                {
                    Buyer = ApplicationUser,
                    BuyerId = ApplicationUser.Id,
                    OrderAddress = new Address
                    {
                        City = "Zagreb",
                        Country = "Croatia",
                        Street = "Ilica 1",
                        Number = "10a"
                    },
                    Message = TestString,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductItem = ProductCategories[0].ProductItems.First(),
                            Quantity = 10,
                            Price = 100,
                           
                        }
                    }

                };

                InMemoryDbContext.Orders.Add(order);
                InMemoryDbContext.SaveChanges();
                orders.Add(order);
            }


            return orders;
        }




        protected List<ProductCategory> GenerateProductCategorys(int number)
        {

            List<ProductCategory> response = new List<ProductCategory>();
            Random random = new Random();

            for (int i = 0; i < number; i++)
            {

                if (i != 0)
                {
                    ProductCategory listItem = new ProductCategory
                    {
                        Description = $"{nameof(ProductCategory.Description)} {random.Next(1, 1000)}",
                        Name = $"{nameof(ProductCategory.Name)} {random.Next(1, 1000)}",
                    };
                    response.Add(listItem);
                }
                else
                {
                    ProductCategory listItem = new ProductCategory
                    {
                        Description = $"{nameof(ProductCategory.Description)} {random.Next(1, 1000)}",
                        Name = $"{TestString} {random.Next(1, 1000)}",
                        ProductItems = new List<ProductItem>()
                        {
                            new ProductItem
                            {
                                Description = TestString,
                                Quantity  = 10,
                                Price = 20,
                                Name = TestString
                            },
                            new ProductItem
                            {
                                Description = TestString,
                                Quantity  = 15,
                                Price = 200,
                                Name = TestString
                            }
                        }
                    };

                    response.Add(listItem);
                }


            }

            InMemoryDbContext.ProductCategorys.AddRange(response);
            InMemoryDbContext.SaveChanges();

            return response;

        }
        protected IProductService GetProductService(ApplicationDbContext? db = null)
        {
            if (db != null)
            {
                return new ProductService(db, Mapper, AppSettings.Object);
            }
            return new ProductService(InMemoryDbContext, Mapper, AppSettings.Object);

        }


        protected IBuyerService GetBuyerService(ApplicationDbContext? db = null)
        {
            if (db != null)
            {
                return new BuyerService(UserManager.Object, db, Mapper);
            }
            return new BuyerService(UserManager.Object, InMemoryDbContext, Mapper);

        }

        private void SetupInMemoryContext()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                            .Options;
            InMemoryDbContext = new ApplicationDbContext(inMemoryOptions);
        }

    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Moq;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Mapping;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
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

        public WebShopSetup()
        {
            SetupInMemoryContext();
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

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class ProductService
    {

        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        /// <summary>
        /// Add product item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductItemViewModel> AddProductItem(ProductItemBinding model)
        {
            var dbo = mapper.Map<ProductItem>(model);
            db.ProductItems.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductItemViewModel>(dbo);
        }
        /// <summary>
        /// Get product item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductItemViewModel> GetProductItem(long id)
        {
            var dbo = await db.ProductItems
                .Include(x => x.ProductCategory)
                .Include(x => x.QuantityType)
                .FirstOrDefaultAsync(y=> y.Id == id);
            return mapper.Map<ProductItemViewModel>(dbo);

        }


        /// <summary>
        /// Delte product item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductItemViewModel> DeleteProductItem(long id)
        {
            var dbo = await db.ProductItems
                .FirstOrDefaultAsync(y => y.Id == id);

            dbo.Valid = false;
            await db.SaveChangesAsync();
            return mapper.Map<ProductItemViewModel>(dbo);

        }

        /// <summary>
        /// Update product item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductItemViewModel> UpdateProductItem(ProductItemUpdateBinding model)
        {
            var dbo = await db.ProductItems.FindAsync(model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductItemViewModel>(dbo);
        }


        //CRUD: ProductCategory,
        //CRUD: ProductItem

    }
}

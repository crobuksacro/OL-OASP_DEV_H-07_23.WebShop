using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private AppSettings appSettings;


        public ProductService(ApplicationDbContext db, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            this.db = db;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
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
                .FirstOrDefaultAsync(y => y.Id == id);
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
        /// <summary>
        /// Get product Categorys
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductCategoryViewModel>> GetProductCategories(bool? valid = true)
        {
            var dbos = await db.ProductCategorys
                .Include(y => y.ProductItems)
                .Where(y => y.Valid == valid).ToListAsync();
            return dbos.Select(y => mapper.Map<ProductCategoryViewModel>(y)).ToList();

        }
        /// <summary>
        /// Update Product Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model)
        {

            var dbo = await db.ProductCategorys.FindAsync(model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }
        /// <summary>
        /// Delete Product Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductCategoryViewModel> DeleteProductCategory(long id)
        {
            var dbo = await db.ProductCategorys.FindAsync(id);
            dbo.Valid = false;
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);

        }
        /// <summary>
        /// Add product Categories by paggination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchTerm"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<ProductCategoryPaginationViewModel> GetProductCategories(int page, string? searchTerm = null, int? offset = null)
        {

            //Nudimo korisniku da sam odabere koliko zeli da prikaze po stranici
            //ukoliko korisnik ne odabere, prikazace se 10 po stranici
            //korsteci appSettings.PaginationOffset
            //koji smo setupirali u appsettings.json
            if (!offset.HasValue || offset.Value > 150)
            {
                offset = appSettings.PaginationOffset;
            }

            //Dohvatimo sve kategorije proizvoda
            var baseQuery = db.ProductCategorys
                .Include(y => y.ProductItems)
                .Where(y => y.Valid);

            //Ukoliko je korisnik uneo search term, filtriramo po nazivu i opisu
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                //koristimo EF.Functions.Like identicno kao sto smo koristili u SQL-u
                baseQuery = baseQuery.Where(s => EF.Functions.Like(s.Name, $"%{searchTerm}%") || EF.Functions.Like(s.Description, $"%{searchTerm}%"));
            }
            //spremamo ukupan broj zapisa
            var totalRecords = await baseQuery.CountAsync();
            //racunamo ukupan broj stranica
            var totalPages = (int)Math.Ceiling((double)totalRecords / offset.Value);
            //Ukoliko korisnik unese stranicu koja ne postoji, vracamo ga na prvu stranicu
            var basePQuery = await baseQuery
                //preskacemo sve zapise koji su na prethodnim stranicama (dohvatima)
                .Skip((page - 1) * offset.Value)
                //uzimamo samo onoliko koliko je korisnik odabrao
                .Take(offset.Value)
                .ToListAsync();

            //sortiramo po datumu kreiranja
            var productCategory = basePQuery.OrderByDescending(y => y.Created).ToList();

            //mapiramo na ViewModel
            var response = new ProductCategoryPaginationViewModel
            {
                ProductCategorys = basePQuery.Select(y => mapper.Map<ProductCategoryViewModel>(y)).ToList(),
                //koliko trenutno imamo zapisa
                Rows = totalPages,
                //koliko ukupno imamo zapisa
                TotalRecords = totalRecords,
            };

            return response;
        }
        /// <summary>
        /// Add new product category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ProductCategoryViewModel> AddCategoryProductItem(ProductCategoryBinding model)
        {
            var company = await db.Companys.FirstOrDefaultAsync(y => y.Valid);
            var dbo = mapper.Map<ProductCategory>(model);
            dbo.Company = company;
            db.ProductCategorys.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }


    }
}

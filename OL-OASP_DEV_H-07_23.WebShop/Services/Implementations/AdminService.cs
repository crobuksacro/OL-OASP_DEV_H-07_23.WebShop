using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.CompanyModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public AdminService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get Company
        /// </summary>
        /// <returns></returns>
        public async Task<CompanyViewModel> GetCompany()
        {
            var company = await db.Companys
                .Include(y=>y.Address)
                .FirstOrDefaultAsync(y => y.Valid);
            return mapper.Map<CompanyViewModel>(company);
        }

        /// <summary>
        /// Update Company
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetCompany <T>()
        {
            var company = await db.Companys
                .Include(y => y.Address)
                .FirstOrDefaultAsync(y => y.Valid);
            return mapper.Map<T>(company);
        }

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<CompanyViewModel> UpdateCompany(CompanyUpdateBinding model)
        {
            var dbo = await db.Companys
                .Include(y => y.Address)
                .FirstOrDefaultAsync(y => y.Id == model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<CompanyViewModel>(dbo);
        }
    }
}

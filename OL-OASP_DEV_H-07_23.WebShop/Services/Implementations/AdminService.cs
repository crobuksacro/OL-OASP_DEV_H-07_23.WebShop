using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.CompanyModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class AdminService
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
            var company = await db.Companys.FirstOrDefaultAsync(y => y.Valid);
            return mapper.Map<CompanyViewModel>(company);
        }


    }
}

using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.CompanyModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IAdminService
    {
        Task<CompanyViewModel> GetCompany();
        Task<CompanyViewModel> UpdateCompany(CompanyUpdateBinding model);
        /// <summary>
        /// Update Company
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetCompany<T>();
    }
}
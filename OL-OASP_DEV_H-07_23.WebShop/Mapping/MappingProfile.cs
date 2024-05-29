using AutoMapper;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategoryUpdateBinding>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryBinding, ProductCategory>();
            CreateMap<ProductCategoryUpdateBinding, ProductCategory>();
            CreateMap<ProductItem, ProductItemViewModel>();
            CreateMap<ProductItemBinding, ProductItem>();
            CreateMap<ProductItemUpdateBinding, ProductItem>();
            CreateMap<QuantityType, QuantityTypeViewModel>();
            //CreateMap<Company, CompanyViewModel>();

        }
    }
}

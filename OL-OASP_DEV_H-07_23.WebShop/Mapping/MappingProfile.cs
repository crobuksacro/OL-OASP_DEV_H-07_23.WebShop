using AutoMapper;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;

namespace OL_OASP_DEV_H_07_23.WebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategoryUpdateBinding>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategory, ProductCategoryUpdateBinding>();
            CreateMap<ProductCategoryBinding, ProductCategory>();
            CreateMap<ProductCategoryUpdateBinding, ProductCategory>();
            CreateMap<ProductItem, ProductItemViewModel>();
            CreateMap<ProductItemBinding, ProductItem>();
            CreateMap<ProductItemUpdateBinding, ProductItem>();
            CreateMap<QuantityType, QuantityTypeViewModel>();
            //CreateMap<Company, CompanyViewModel>();

            CreateMap<Address, AddressViewModel>();
            CreateMap<Address, AddressUpdateBinding>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>();

            CreateMap<AddressBinding, Address>();

            CreateMap<OrderUpdateBinding, Order>();
            CreateMap<OrderBinding, Order>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItemBinding, OrderItem>();
            CreateMap<OrderItemUpdateBiding, OrderItem>();
            CreateMap<OrderItem, OrderItemViewModel>();
        }
    }
}

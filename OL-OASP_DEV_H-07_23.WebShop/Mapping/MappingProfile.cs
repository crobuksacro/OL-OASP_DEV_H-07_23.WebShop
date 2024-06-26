﻿using AutoMapper;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.CompanyModels;
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
            CreateMap<Address, AddressViewModel>();
            CreateMap<Address, AddressUpdateBinding>();
            CreateMap<Address, AddressBinding>();
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ApplicationUser, ApplicationUserUpdateBinding>();
            CreateMap<ApplicationUserUpdateBinding, ApplicationUser>();
            CreateMap<AddressBinding, Address>();
            CreateMap<AddressUpdateBinding, Address>();
            CreateMap<OrderUpdateBinding, Order>();
            CreateMap<OrderBinding, Order>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItemBinding, OrderItem>();
            CreateMap<OrderItemUpdateBiding, OrderItem>();
            CreateMap<OrderItem, OrderItemViewModel>();
            CreateMap<BuyerFeedbackBinding, BuyerFeedback>();
            CreateMap<BuyerFeedback, BuyerFeedbackViewModel>();

            CreateMap<Company, CompanyViewModel>();
            CreateMap<Company, CompanyUpdateBinding>();
            CreateMap<CompanyUpdateBinding, Company>();

        }
    }
}

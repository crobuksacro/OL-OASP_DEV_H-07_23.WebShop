namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels
{
    public class ProductCategoryPaginationViewModel
    {
        public int TotalRecords { get; set; }
        public int Rows { get; set; }
        public List<ProductCategoryViewModel> ProductCategorys { get; set; }
    }
}

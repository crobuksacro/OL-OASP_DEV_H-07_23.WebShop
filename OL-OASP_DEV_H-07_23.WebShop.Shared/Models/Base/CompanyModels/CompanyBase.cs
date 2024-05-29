namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.CompanyModels
{
    public abstract class CompanyBase
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        /// <summary>
        /// Oib
        /// </summary>
        public string VAT { get; set; }

    }
}

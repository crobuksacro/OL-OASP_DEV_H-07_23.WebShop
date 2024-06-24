namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.Common
{
    public abstract class SessionItemBase
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Expires { get; set; }
    }
}

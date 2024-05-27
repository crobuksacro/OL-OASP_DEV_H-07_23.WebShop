namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces
{
    public interface IBaseTableAtributes
    {
        DateTime Created { get; set; }
        long Id { get; set; }
        DateTime Updated { get; set; }
        bool Valid { get; set; }
    }
}

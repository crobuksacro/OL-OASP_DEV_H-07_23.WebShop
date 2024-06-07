namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto
{
    public class AppSettings
    {
        public int PaginationOffset { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}

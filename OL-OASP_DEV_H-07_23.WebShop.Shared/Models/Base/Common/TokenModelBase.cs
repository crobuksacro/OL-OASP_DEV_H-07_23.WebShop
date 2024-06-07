using System.Text.Json.Serialization;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.Common
{
    public abstract class TokenModelBase
    {
        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }
}

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Buyer = "Buyer";
    }


    public enum OrderStatus
    {
        /// <summary>
        /// Narudžba je primljena, ali još nije obrađena
        /// </summary>
        Pending,
        /// <summary>
        /// Narudžba se obrađuje
        /// </summary>
        Processing,
        /// <summary>
        /// Narudžba je poslana
        /// </summary>
        Shipped,
        /// <summary>
        /// Narudžba je isporučena
        /// </summary>
        Delivered,
        /// <summary>
        /// Narudžba je otkazana
        /// </summary>
        Canceled,
        /// <summary>
        /// Narudžba je vraćena
        /// </summary>
        Returned,
        /// <summary>
        /// Narudžba je refundirana
        /// </summary>
        Refunded       
    }
}

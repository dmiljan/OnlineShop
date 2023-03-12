using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.DTOs.Requests.Queries
{
    public class GetAllProductFilter
    {
        [FromQuery(Name = "productTypeId")]
        public int ProductTypeId { get; set; }

        [FromQuery(Name = "priceFrom")]
        public float PriceFrom { get; set; }

        [FromQuery(Name = "priceTo")]
        public float PriceTo { get; set; }
    }
}

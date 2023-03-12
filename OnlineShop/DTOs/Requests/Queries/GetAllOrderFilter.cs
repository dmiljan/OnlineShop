using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.DTOs.Requests.Queries
{
    public class GetAllOrderFilter
    {
        [FromQuery(Name = "processed")]
        public bool Processed { get; set; }

        [FromQuery(Name = "totalPriceFrom")]
        public float TotalPriceFrom { get; set; }

        [FromQuery(Name = "totalPriceTo")]
        public float TotalPriceTo { get; set; }
    }
}

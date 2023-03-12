namespace OnlineShop.DTOs.Requests
{
    public class ProductRequestDto
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public List<ProductAttributeRequestDto> ProductAttribute { get; set; }
    }
}

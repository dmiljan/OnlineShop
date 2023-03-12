using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IProductImage
    {
        Task<ProductImage> AddImage(ProductImage image);
        Task<ProductImage> GetImageById(int id);
        Task DeleteImage(ProductImage image);
    }
}

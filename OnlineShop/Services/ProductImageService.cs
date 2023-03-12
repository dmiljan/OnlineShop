using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class ProductImageService : IProductImage
    {
        private DataContext _context;

        public ProductImageService(DataContext context)
        {
            _context = context;
        }

        public async Task<ProductImage> AddImage(ProductImage image)
        {
            await _context.ProductImage.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task DeleteImage(ProductImage image)
        {
            _context.ProductImage.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductImage> GetImageById(int id)
        {
            var image = await _context.ProductImage.FindAsync(id);

            return image;
        }
    }
}

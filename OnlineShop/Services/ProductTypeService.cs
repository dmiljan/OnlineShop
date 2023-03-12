using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class ProductTypeService : IProductType
    {
        private DataContext _context;

        public ProductTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<ProductType> AddProductType(ProductType productType)
        {
            await _context.ProductType.AddAsync(productType);
            await _context.SaveChangesAsync();

            return productType;
        }

        public async Task DeleteProductType(ProductType productType)
        {
            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductType> GetProductTypeById(int id)
        {
            var productType = await _context.ProductType.FindAsync(id);

            return productType;
        }

        public async Task<List<ProductType>> GetProductTypes()
        {
            var productTypes = await _context.ProductType.ToListAsync();

            return productTypes;
        }

        public async Task UpdateNameOfProductType(ProductType productType)
        {
            var existingProductType = await _context.ProductType.FindAsync(productType.Id);

            if(existingProductType != null)
            {
                existingProductType.Name = productType.Name;

                _context.ProductType.Update(existingProductType);
                await _context.SaveChangesAsync();
            }
        }
    }
}

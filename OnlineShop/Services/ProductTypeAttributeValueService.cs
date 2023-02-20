using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class ProductTypeAttributeValueService : IProductTypeAttributeValue
    {
        private DataContext _context;

        public ProductTypeAttributeValueService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ProductTypeAttributeValue>> AddAtributesValueToProductType(List<ProductTypeAttributeValue> ProductTypeAttributeValues)
        {
            await _context.ProductTypeAttributeValue.AddRangeAsync(ProductTypeAttributeValues);
            await _context.SaveChangesAsync();

            return ProductTypeAttributeValues.ToList();
        }

        public async Task DeleteAssignedAttributeValueToProductType(ProductTypeAttributeValue productTypeAttributeValue)
        {
            _context.ProductTypeAttributeValue.Remove(productTypeAttributeValue);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductTypeAttributeValue>> GetAllAttributeValuesForProductType(int productTypeId)
        {
            var productTypeAttributeValues = await _context.ProductTypeAttributeValue
                .Where(x => x.ProductTypeId == productTypeId)
                .Include(x => x.AttributeValue)
                    .ThenInclude(a => a.Attribute)
                .ToListAsync();

            return productTypeAttributeValues;
        }

        public async Task<ProductTypeAttributeValue> GetPoductTypeAttributeValueById(int productTypeId, int attributeValueId)
        {
            var productTypeAttributeValue = await _context.ProductTypeAttributeValue
                .Where(x => x.ProductType.Id == productTypeId && x.AttributeValueId == attributeValueId)
                .FirstOrDefaultAsync();

            return productTypeAttributeValue;
        }
    }
}

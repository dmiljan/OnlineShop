using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class ProductTypeAttributeService : IProductTypeAttribute
    {
        private DataContext _context;

        public ProductTypeAttributeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductTypeAttribute>> AddAtributesToProductType(List<ProductTypeAttribute> listProductTypeAttribute)
        {
            foreach(var obj in listProductTypeAttribute)
            {
                await _context.ProductTypeAttribute.AddAsync(obj);
            }
            await _context.SaveChangesAsync();

            return listProductTypeAttribute.ToList();
        }

        public async Task DeleteAssignedAttributeToProductType(ProductTypeAttribute productTypeAttribute)
        {
            _context.ProductTypeAttribute.Remove(productTypeAttribute);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductTypeAttribute>> GetAllAttributesForProductType(int productTypeId)
        {
            var productTypeAttributes = await _context.ProductTypeAttribute
                .Where(x => x.ProductTypeId == productTypeId)
                .Include(a => a.Attribute)
                .ToListAsync();

            return productTypeAttributes;
        }

        public async Task<ProductTypeAttribute> GetPoductTypeAttributeById(int productTypeId, int attributeId)
        {
            var productTypeAttribute = await _context.ProductTypeAttribute
                .Where(x => x.ProductType.Id == productTypeId && x.AttributeId == attributeId)
                .FirstOrDefaultAsync();

            return productTypeAttribute;
        }

    }
}

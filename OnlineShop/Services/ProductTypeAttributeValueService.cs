using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineShop.Data;
using OnlineShop.DTOs.Responses;
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

        public async Task<IEnumerable<AttributeValueForProductTypeResponseDto>> GetAllAttributeValuesForProductType(int productTypeId)
        {
            var productTypeAttributeValues = (await _context.ProductTypeAttributeValue
                .Where(x => x.ProductTypeId == productTypeId)
                .Select(x => new
                {
                    x.AttributeValue.AttributeId,
                    x.AttributeValue.Attribute.Name,
                    x.AttributeValueId,
                    x.AttributeValue.Value
                })
                .ToListAsync()
                ).GroupBy(x => x.AttributeId)
                .Select(x => new AttributeValueForProductTypeResponseDto
                {
                    AttributeName = x.FirstOrDefault().Name,
                    Values = x.Select(y => new AttributeValuesForDto
                    {
                        AttributeValueId = y.AttributeValueId,
                        Value = y.Value
                    })
                });

            return productTypeAttributeValues;
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

        public async Task<ProductTypeAttributeValue> GetPoductTypeAttributeValueById(int productTypeId, int attributeValueId)
        {
            var productTypeAttributeValue = await _context.ProductTypeAttributeValue
                .Where(x => x.ProductType.Id == productTypeId && x.AttributeValueId == attributeValueId)
                .FirstOrDefaultAsync();

            return productTypeAttributeValue;
        }
    }
}

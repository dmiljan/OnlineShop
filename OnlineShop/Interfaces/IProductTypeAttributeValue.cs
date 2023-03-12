using OnlineShop.DTOs.Responses;
using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IProductTypeAttributeValue
    {
        Task<List<ProductTypeAttributeValue>> AddAtributesValueToProductType(List<ProductTypeAttributeValue> productTypeAttributeValue);
        Task DeleteAssignedAttributeValueToProductType(ProductTypeAttributeValue productTypeAttributeValue);
        Task<ProductTypeAttributeValue> GetPoductTypeAttributeValueById(int productTypeId, int attributeValueId);
        Task<IEnumerable<AttributeValueForProductTypeResponseDto>> GetAllAttributeValuesForProductType(int productTypeId);
    }
}

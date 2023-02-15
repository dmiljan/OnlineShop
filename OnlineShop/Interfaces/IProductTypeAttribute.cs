using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IProductTypeAttribute
    {
        Task<List<ProductTypeAttribute>> AddAtributesToProductType(List<ProductTypeAttribute> productTypeAttribute);
        Task DeleteAssignedAttributeToProductType(ProductTypeAttribute productTypeAttribute);
        Task<ProductTypeAttribute> GetPoductTypeAttributeById(int productTypeId, int attributeId);
        Task<List<ProductTypeAttribute>> GetAllAttributesForProductType(int productTypeId);
    }
}

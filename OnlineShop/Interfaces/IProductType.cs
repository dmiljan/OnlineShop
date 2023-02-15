using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IProductType
    {
        Task<List<ProductType>> GetProductTypes();
        Task<ProductType> GetProductTypeById(int id);
        Task<ProductType> AddProductType(ProductType productType);
        Task UpdateNameOfProductType(ProductType productType);
        Task DeleteProductType(ProductType productType);
    }
}

using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IProduct
    {
        Task<Product> AddProduct(ProductRequestDto productRequestDto);
        Task<ProductResponseDto> GetProductById(int id);
        Task<IEnumerable<ProductResponseDto>> GetProducts(GetAllProductFilter filter = null, PaginationQuery paginationQuery = null);
        Task DeleteProduct(Product product);
        Task UpdateProduct(Product product);

    }
}

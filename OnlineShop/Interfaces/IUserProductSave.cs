using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IUserProductSave
    {
        Task<UserProductSave> UserSaveProduct(UserProductSave userProductSave);
        Task<IEnumerable<ProductResponseDto>> GetAllSavedProducts(string UserId, PaginationQuery paginationQuery = null);
        Task DeleteSavedProduct(UserProductSave userProductSave);
    }
}

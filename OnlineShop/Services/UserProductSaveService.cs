using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class UserProductSaveService : IUserProductSave
    {
        private DataContext _context;

        public UserProductSaveService(DataContext context)
        {
            _context = context;
        }

        public async Task<UserProductSave> UserSaveProduct(UserProductSave userProductSave)
        {
            await _context.UserProductSave.AddAsync(userProductSave);
            await _context.SaveChangesAsync();
            return userProductSave;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllSavedProducts(string UserId, PaginationQuery paginationQuery = null)
        {
            var products = await _context.Product
                .Where(x => x.Users.Where(x => x.UserId == UserId).Any())
                .Select(x => new ProductResponseDto
                {
                    Product = x,
                    ProductType = x.ProductType.Name,
                    Attributes = x.ProductAttribute.Select(y => new AttributeForDto
                    {
                        AttributeId = y.AttributeId,
                        AttributeName = y.Attribute.Name,
                        Value = y.AttributeValue.Label ?? y.Value,
                        ValueType = y.Attribute.ValueType,
                        InputType = y.Attribute.InputType
                    }),
                    Images = x.ProductImage.Select(z => z.ImagePath)
                }).ToListAsync();

            if(paginationQuery == null)
            {
                return products;
            }

            var skip = (paginationQuery.PageNumber - 1 ) * paginationQuery.PageSize;

            return products
                .Skip(skip)
                .Take(paginationQuery.PageSize);
        }

        public async Task DeleteSavedProduct(UserProductSave userProductSave)
        {
            _context.UserProductSave.Remove(userProductSave);
            await _context.SaveChangesAsync();
        }
    }
}

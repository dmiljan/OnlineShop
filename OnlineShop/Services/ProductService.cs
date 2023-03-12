using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Requests.Queries;
using OnlineShop.DTOs.Responses;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class ProductService : IProduct
    {
        private DataContext _context;
        public IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> AddProduct(ProductRequestDto productRequestDto)
        {
            var product = _mapper.Map<Product>(productRequestDto);

            await _context.Product.AddAsync(product); //Data will be saved in Product and ProductAttribute table
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProducts(GetAllProductFilter filter = null, PaginationQuery paginationQuery = null)
        {
            var products = await _context.Product
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

                })
                .ToListAsync();

            if (paginationQuery == null)
            {
                return products;
            }

            //products filtering
            products = AddFiltersOnQuery(filter, products).ToList();

            var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;
            
            return products
                .Skip(skip)
                .Take(paginationQuery.PageSize);
        }

        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var products = await _context.Product
                .Where(x => x.Id == id)
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
                }).FirstOrDefaultAsync();

            return products;
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var existingProduct = await _context.Product.FindAsync(product.Id);
            if(existingProduct != null)
            {
                existingProduct.ProductTypeId = product.ProductTypeId;
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Model = product.Model;

                _context.Product.Update(existingProduct);
                await _context.SaveChangesAsync();
            }

        }

        private static IEnumerable<ProductResponseDto> AddFiltersOnQuery(GetAllProductFilter filter, IEnumerable<ProductResponseDto> products)
        {
            if (filter?.ProductTypeId != 0)
            {
                products = products.Where(x => x.Product.ProductTypeId == filter.ProductTypeId).ToList();
            }
            if(filter?.PriceFrom != 0 && filter?.PriceTo != 0)
            {
                products = products.Where(x => x.Product.Price >= filter.PriceFrom && x.Product.Price <= filter.PriceTo).ToList();
            }
            return products;
        }
    }
}

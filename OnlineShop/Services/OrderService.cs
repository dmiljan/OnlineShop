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
    public class OrderService : IOrder
    {
        private DataContext _context;
        public IMapper _mapper;

        public OrderService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Order> AddOrder(OrderRequestDto order)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                float totalPrice = 0;
                foreach (var obj in order.ProductQuantity)
                {
                    var product = await _context.Product.FindAsync(obj.ProductId);

                    //Calculation total price of the order.
                    totalPrice += product.Price * obj.Quantity;

                    //Reducing quantity of products in product table when order is created.
                    product.Quantity -= obj.Quantity;
                    _context.Product.Update(product);
                }

                //Create new order.
                var _order = _mapper.Map<Order>(order);
                _order.TotalPrice = totalPrice;

                _order.Products = order.ProductQuantity.Select(x => new OrderProduct()
                {
                    //OrderId automatically
                    ProductId = x.ProductId,
                    QuantityInCart = x.Quantity
                }).ToList();

                await _context.Order.AddAsync(_order);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return _order;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<OrderResponseDto> GetOrderWithAllProducts(int id)
        {
            var order = await _context.Order
                .Where(or => or.Id == id)
                .Select(x => new OrderResponseDto
                {
                    Order = x,
                    UserDetails = new UserForDto
                    {
                        Email = x.User.Email,
                        Name = $"{x.User.FirstName} {x.User.LastName}",
                    },
                    Products = x.Products.Select(y => new ProductForDto
                    {
                        Name = y.Product.Name,
                        Model = y.Product.Model,
                        QuantityInCart = y.QuantityInCart,
                        Price = y.Product.Price,
                        ProductType = y.Product.ProductType.Name,
                        Images = y.Product.ProductImage.Select(z => z.ImagePath)
                    }) 
                })
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrders(GetAllOrderFilter filter = null, PaginationQuery paginationQuery = null)
        {
            var orders = _context.Order
                .Select(x => new OrderResponseDto
                {
                    Order = x,
                    UserDetails = new UserForDto
                    {
                        Email = x.User.Email,
                        Name = $"{x.User.FirstName} {x.User.LastName}",
                    },
                    Products = x.Products.Select(y => new ProductForDto
                    {
                        Name = y.Product.Name,
                        Model = y.Product.Model,
                        QuantityInCart = y.QuantityInCart,
                        Price = y.Product.Price,
                        ProductType = y.Product.ProductType.Name,
                        Images = y.Product.ProductImage.Select(z => z.ImagePath)
                    })
                });

            if(paginationQuery == null)
            {
                return await orders.ToListAsync();
            }

            //Orders filtering
            var result = await AddFiltersOnQuery(filter, orders).ToListAsync();

            var skip = (paginationQuery.PageNumber - 1) * paginationQuery.PageSize;
            
            return result
                .Skip(skip)
                .Take(paginationQuery.PageSize);
        }

        /// <summary>
        /// Order is created and field Processed is set on false.
        /// When an employee processes the order, field Processed will be set on true.
        /// </summary>
        public async Task CompleteOrder(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);
            order.Processed = true;

            _context.Order.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Order order)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }

        private static IQueryable<OrderResponseDto> AddFiltersOnQuery(GetAllOrderFilter filter, IQueryable<OrderResponseDto> orders)
        {
            if (filter?.Processed != null)
            {
                orders = orders.Where(x => x.Order.Processed == filter.Processed);
            }
            if (filter?.TotalPriceFrom != 0)
            {
                orders = orders.Where(x => x.Order.TotalPrice >= filter.TotalPriceFrom);
            }
            if (filter?.TotalPriceTo != 0)
            {
                orders = orders.Where(x => x.Order.TotalPrice <= filter.TotalPriceTo);
            }
            return orders;
        }
    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.domain.Entities.PetProducts.PetProduct;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderRepository(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> AddProductOrder(Guid productId, int quantity)
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (customerId != null)
        {
            var orderProduct = OrderProduct.Create(
                CustomerId.Create(Guid.Parse(customerId))
                , ProductId.Create(productId)
                , quantity);
            await _dbContext.AddAsync(orderProduct);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task AddOrder(Order order)
    {
        await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<OrderProductWithPrice>?> RetrieveTotalOrderProductPaymentInCart(OrderId orderId)
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId != null)
        {
            var totalProductPayment = await _dbContext.OrderProducts
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Create(Guid.Parse(customerId)) && o.OrderProductStatus == OrderProductStatus.Ordered)
                .Join(
                    _dbContext.Products,
                    orderProduct => orderProduct.ProductId,
                    product => product.Id,
                    (orderProduct, product) => new OrderProductWithPrice(
                        OrderProduct.Retrive(
                            orderProduct.Id,
                            orderProduct.CustomerId,
                            orderProduct.ProductId,
                            orderProduct.Quantity,
                            orderProduct.OrderProductStatus
                        ),
                        product.ProductPrice.Value
                    )
                ).ToListAsync();

            // Update OrderProductStatus to Paid
            foreach (var orderProductWithPrice in totalProductPayment)
            {
                var orderProduct = orderProductWithPrice.OrderProduct;

                // Assuming you have a method to update the OrderProductStatus in your _dbContext
                orderProduct.CompletedOrder(orderId);

                // Update the orderProduct status in the context
                _dbContext.Entry(orderProduct).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync(); // Save changes to the database

            return totalProductPayment;
        }
        return null;
    }

    public async Task<List<OrderProduct>?> RetrieveOrderedProductsForUser()
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (customerId != null)
        {
            var orderProducts = await _dbContext.OrderProducts
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Create(Guid.Parse(customerId)) && o.OrderProductStatus == OrderProductStatus.Ordered)
                .Include(o => o.Product)
                .Select(op => OrderProduct.RetriveOrderProductBrief(
                    Product.ProductBrief(
                        op.Product.ProductName,
                        op.Product.ProductDetail,
                        op.Product.ProductPrice.Value,
                        op.Product.ImageData ?? Array.Empty<byte>()
                    ),
                    op.Quantity
                ))
                .ToListAsync();
            if (orderProducts.Count > 0)
                return orderProducts;
            return null;
        }
        return null!;
    }


    public async Task<OrderProduct?> CheckOrderIsExist(Guid orderProductId)
    {
        var order = await _dbContext.OrderProducts
            .Where(o => o.ProductId == ProductId.Create(orderProductId) && o.OrderProductStatus == OrderProductStatus.Ordered)
            .FirstOrDefaultAsync();
        return order;
    }

    public async Task UpdateOrderProduct(OrderProduct orderProduct)
    {
        _dbContext.Update(orderProduct);
        await _dbContext.SaveChangesAsync();
    }
}
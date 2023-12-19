using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _dbContext;
    // private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderRepository(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        // _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddProductOrder(Guid customerId, Guid productId, int quantity)
    {
        var orderProduct = OrderProduct.Create(
            CustomerId.Create(customerId)
            , ProductId.Create(productId)
            , quantity);
        await _dbContext.AddAsync(orderProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddOrder(Order order)
    {
        await _dbContext.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductPaymentInCart(Guid customerId, Guid orderId)
    {
        var order = pet_store_backend.domain.Entities.Orders.Order.CreateOrder(orderId);
        await _dbContext.AddAsync(order);
        var orderProducts = await _dbContext.OrderProducts
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Create(customerId) && o.OrderProductStatus == OrderProductStatus.Ordered)
            .ToListAsync();

        // Update OrderProductStatus to Paid
        foreach (var orderProduct in orderProducts)
        {
            // Assuming you have a method to update the OrderProductStatus in your _dbContext
            orderProduct.CompletedOrder(OrderId.Create(orderId));

            // Update the orderProduct status in the context
            _dbContext.Entry(orderProduct).State = EntityState.Modified;
        }
        await _dbContext.SaveChangesAsync(); // Save changes to the database
    }

    public async Task UpdateOrderStatusAccept(Guid orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId.Create(orderId));
        if (order is not null)
        {
            order.UpdateOrderAccept();
            _dbContext.Entry(order).State = EntityState.Modified;
        }
        await _dbContext.SaveChangesAsync(); // Save changes to the database
    }

    public async Task<List<OrderBriefResult>?> RetrieveOrderedProductsForCustomer(Guid customerId)
    {
        var orderProducts = await _dbContext.OrderProducts
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Create(customerId) && o.OrderProductStatus == OrderProductStatus.Ordered)
            .Include(o => o.Product)
            .Select(op => new OrderBriefResult(
                op.Id.Value,
                new ProductOrderBriefResult(
                    op.ProductId.Value,
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


    public async Task<OrderProduct?> CheckProductOrderIsExist(Guid productId, Guid customerId)
    {
        var order = await _dbContext.OrderProducts
            .Where(o => o.ProductId == ProductId.Create(productId) && o.CustomerId == CustomerId.Create(customerId) && o.OrderProductStatus == OrderProductStatus.Ordered)
            .FirstOrDefaultAsync();
        return order;
    }

    public async Task<OrderProduct?> CheckOrderIsExist(Guid orderProductId, Guid customerId)
    {
        var order = await _dbContext.OrderProducts
            .Where(o => o.Id == OrderProductId.Create(orderProductId) && o.CustomerId == CustomerId.Create(customerId) && o.OrderProductStatus == OrderProductStatus.Ordered)
            .FirstOrDefaultAsync();
        return order;
    }

    public async Task UpdateOrderProduct(OrderProduct orderProduct)
    {
        _dbContext.Entry(orderProduct).State = EntityState.Modified;
        // Save changes to the database
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteOrderProduct(OrderProduct orderProduct)
    {
        _dbContext.OrderProducts.Remove(orderProduct);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OrderProduct?> RetrieveOrderProduct(Guid orderProductId)
    {
        var orderProduct = await _dbContext.OrderProducts.FirstOrDefaultAsync(
            orderProduct => orderProduct.Id == OrderProductId.Create(orderProductId));
        return orderProduct;
    }
}
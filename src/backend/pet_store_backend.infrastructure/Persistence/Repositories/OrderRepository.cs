using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Order.Common;
using pet_store_backend.application.PetProducts.Common;
using pet_store_backend.domain.Entities.Orders;
using pet_store_backend.domain.Entities.Orders.ValueObjects;
using pet_store_backend.domain.Entities.PetProducts.ValueObjects;
using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.infrastructure.Persistence.Common;

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

    public async Task UpdateProductPaymentE_WalletInCart(Guid customerId, Guid orderId)
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

    public async Task UpdateProductPaymentCODInCart(Guid customerId, Guid orderId)
    {
        var order = pet_store_backend.domain.Entities.Orders.Order.CreateOrder(orderId);
        order.UpdatePaymentStatus(PaymentStatus.COD);
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

    public async Task UpdateOrderE_WalletStatusAccept(Guid orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId.Create(orderId));
        if (order is not null)
        {
            order.UpdatePaymentStatus(PaymentStatus.E_Wallet);
            _dbContext.Entry(order).State = EntityState.Modified;
        }
        await _dbContext.SaveChangesAsync(); // Save changes to the database
    }

    // public async Task UpdateOrderCODStatusAccept(Guid orderId)
    // {
    //     var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId.Create(orderId));
    //     if (order is not null)
    //     {
    //         order.UpdatePaymentStatus(PaymentStatus.COD);
    //         _dbContext.Entry(order).State = EntityState.Modified;
    //     }
    //     await _dbContext.SaveChangesAsync(); // Save changes to the database
    // }

    public async Task UpdateOrderStatusCancelled(Guid orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId.Create(orderId));
        if (order is not null)
        {
            order.UpdatePaymentStatus(PaymentStatus.Cancelled);
            _dbContext.Entry(order).State = EntityState.Modified;
        }
        await _dbContext.SaveChangesAsync(); // Save changes to the database
    }

    public async Task<List<OrderProductBriefResult>?> RetrieveOrderedProductsForCustomer(Guid customerId)
    {
        var orderProducts = await _dbContext.OrderProducts
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Create(customerId) && o.OrderProductStatus == OrderProductStatus.Ordered)
            .Include(o => o.Product)
            .Select(op => new OrderProductBriefResult(
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

    public async Task<List<OrderResult>> RetrieveOrderHistory(Guid customerId, int page)
    {
        int pageSize = PageSize.OrderSize;

        // Calculate the number of products to skip based on the page number
        int ordersToSkip = (page - 1) * pageSize;
        // Assuming _dbContext is your DbContext with DbSet<Order> and DbSet<OrderProduct>
        var ordersWithProducts = await _dbContext.Orders
            .OrderByDescending(order => order.OrderDate)  // Add an OrderBy clause
            .AsNoTracking()
            .Skip(ordersToSkip)
            .GroupJoin(
                _dbContext.OrderProducts.Where(orderProduct => orderProduct.CustomerId == CustomerId.Create(customerId)),
                order => order.Id,
                orderProduct => orderProduct.OrderId,
                (order, orderProducts) => new OrderResult(
                    order.OrderDate,
                    order.PaymentStatus,
                    order.OrderProducts.Select(op => new OrderProductBriefResult(
                        op.Id.Value,
                        new ProductOrderBriefResult(
                        op.ProductId.Value,
                        op.Product.ProductName,
                        op.Product.ProductDetail,
                        op.Product.ProductPrice.Value,
                        op.Product.ImageData ?? Array.Empty<byte>()
                    ),
                    op.Quantity
                    )
                )
            ))
            .ToListAsync();
        return ordersWithProducts;
    }
}
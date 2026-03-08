using Dapper;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly DataContext.DataContext _connection = new();


    public async Task<List<Order>> GetAllAsync()
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM orders";
        var orders = await context.QueryAsync<Order>(sql);

        return orders.ToList();
    }
    public async Task<List<Order>> GetCompanyOrdersAsync(int companyId)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM orders WHERE company_id= @companyId";
        var orders = await context.QueryAsync<Order>(sql,new {companyId});

        return orders.ToList();
    }
    public async Task<string> AddAsync(Order order)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = """
                    INSERT INTO orders (
                    company_id,
                    order_date,
                    status,
                    total_amount, 
                    created_at, 
                    updated_at
                    ) 
                    VALUES (
                    @CompanyId,
                    @OrderDate,
                    @Status,
                    @TotalAmount, 
                    @CreatedAt, 
                    @UpdatedAt
                    )
                  """;

        var result = await context.ExecuteAsync(sql, order);
        return result == 0
            ? "Failed to add Order."
            : "Order added successfully.";
    }

    public async Task<string> UpdateAsync(int id, Order order)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM orders WHERE id = @Id";

            var orderExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
            if (orderExists != 1)
            {
                return "Order not found.";
            }

            var sql2 = """
                       UPDATE orders
                       SET
                       company_id = @CompanyId,
                       order_date =  @OrderDate,
                       status = @Status,
                       total_amount = @TotalAmount, 
                       updated_at = NOW()
                       WHERE id = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                Company_id = order.CompanyId,
                Order_date = order.OrderDate,
                Status = order.Status,
                Total_amount = order.TotalAmount,
                Created_at = order.CreatedAt,
                Updated_at =DateTime.Now
            });
            return result == 0
                ? "Failed to update Order."
                : "Order updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the Order : {e.Message}";
        }
    }

    public async Task<string> DeleteAsync(int id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql1 = "SELECT 1 FROM orders WHERE id = @Id";

        var orderExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
        if (orderExists != 1)
        {
            return "Order not found.";
        }

        var sql2 = "DELETE FROM orders WHERE id = @Id";
        var result = await context.ExecuteAsync(sql2, new { id });
        return result == 0
            ? "Failed to delete Order."
            : "Order deleted successfully.";
    }

    public async Task<string> UpdateOrderStatusAsync(int id, string status)
    {
         try{
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM orders WHERE id = @Id";

            var orderExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id,status });
            if (orderExists != 1)
            {
                return "Order not found.";
            }

            var sql2 = """
                       UPDATE orders
                       SET
                       status = @Status,
                       updated_at = NOW()
                       WHERE id = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                Status = status,
                Updated_at =DateTime.Now
            });

            return result == 0
                ? "Failed to update Order."
                : "Order updated successfully.";
         }catch (Exception e)
        {
            return $"An error occurred while updating the Order : {e.Message}";
        }
    }
}

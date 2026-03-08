using Dapper;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class OrderItemService:IOrderItemService
{
    private readonly DataContext.DataContext _connection = new();


    public async Task<List<OrderItem>> GetAllAsync()
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM orderItems";
        var orderItems = await context.QueryAsync<OrderItem>(sql);

        return orderItems.ToList();
    }

    public async Task<string> AddAsync(OrderItem orderItem)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = """
                    INSERT INTO orderItems (
                     order_id,
                     menu_item_id,
                     quantity,
                     price,
                     created_at,
                     updated_at
                     ) 
                    VALUES (
                     @Order_id,
                     @Menu_item_id,
                     @Quantity,
                     @Price,
                     @Created_at,
                     @Updated_at
                     )
                  """;

        var result = await context.ExecuteAsync(sql, orderItem);
        return result == 0
            ? "Failed to add orderItem."
            : "orderItem added successfully.";
    }

    public async Task<string> UpdateAsync(int id, OrderItem orderItem)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM orderItems WHERE id = @Id";

            var orderItemExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
            if (orderItemExists != 1)
            {
                return "orderItem not found.";
            }

            var sql2 = """
                       UPDATE orderItems
                       SET
                       order_id = @Order_id,
                       menu_item_id = @Menu_item_id,
                       quantity = @Quantity,
                       price = @Price,
                       updated_at = NOW()
                       WHERE id = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                Order_id = orderItem.OrderId,
                Menu_item_id = orderItem.MenuItemId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                Updated_at = DateTime.Now
            });
            return result == 0
                ? "Failed to update orderItem."
                : "orderItem updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the orderItem : {e.Message}";
        }
    }

    public async Task<string> DeleteAsync(int id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql1 = "SELECT 1 FROM orderItems WHERE id = @Id";

        var orderItemExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
        if (orderItemExists != 1)
        {
            return "orderItem not found.";
        }

        var sql2 = "DELETE FROM orderItems WHERE id = @Id";
        var result = await context.ExecuteAsync(sql2, new { id });
        return result == 0
            ? "Failed to delete OrderItem."
            : "OrderItem deleted successfully.";
    }
}

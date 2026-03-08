using Dapper;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class MenuItemService : IMenuItemService
{
    private readonly DataContext.DataContext _connection = new();


    public async Task<List<MenuItem>> GetAllAsync()
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM menu_Items";
        var menu_Items = await context.QueryAsync<MenuItem>(sql);

        return menu_Items.ToList();
    }
     public async Task<List<MenuItem>> GetByCategoryAsync(string category)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM menu_Items WHERE category=@Category";
        var menu_Items = await context.QueryAsync<MenuItem>(sql,new{category});

        return menu_Items.ToList();
    }

    public async Task<string> AddAsync(MenuItem menuItem)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = """
                    INSERT INTO Menu_Items (menu_id,name, description, price, category, created_at, updated_at) 
                    VALUES (@Menu_id,@Name, @Description, @Price, @Category, @Created_at, @Updated_at)
                  """;

        var result = await context.ExecuteAsync(sql, menuItem);
        return result == 0
            ? "Failed to add MenuItem."
            : "MenuItem added successfully.";
    }

    public async Task<string> UpdateAsync(int id, MenuItem menuItem)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM menu_Items WHERE id = @Id";

            var menuItemExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
            if (menuItemExists != 1)
            {
                return "menuItem not found.";
            }

            var sql2 = """
                    UPDATE menu_Items
                    SET
                    menu_id= @Menu_id,
                    name=@Name,
                    description= @Description,
                    price=@Price, 
                    category=@Category,
                    updated_at = NOW()
                    WHERE id = @Id
                    """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                Menu_id = menuItem.MenuId,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Category = menuItem.Category,
                Updated_at = DateTime.Now
            });
            return result == 0
                ? "Failed to update menuItem."
                : "menuItem updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the menuItem : {e.Message}";
        }
    }

    public async Task<string> DeleteAsync(int id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql1 = "SELECT 1 FROM menu_Items WHERE id = @Id";

        var menuItemExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
        if (menuItemExists != 1)
        {
            return "menuItem not found.";
        }

        var sql2 = "DELETE FROM menu_Items WHERE id = @Id";
        var result = await context.ExecuteAsync(sql2, new { id });
        return result == 0
            ? "Failed to delete menuItem."
            : "menuItem deleted successfully.";
    }
}

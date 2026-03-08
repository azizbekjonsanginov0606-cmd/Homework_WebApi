using Dapper;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class MenuService : IMenuService
{
    private readonly DataContext.DataContext _connection = new();


    public async Task<List<Menu>> GetAllAsync()
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM Menus";
        var menus = await context.QueryAsync<Menu>(sql);

        return menus.ToList();
    }
    public async Task<List<Menu>> GetByDateAsync(DateTime date)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM Menus WHERE menu_date= @MenuDate";
        var menus = await context.QueryAsync<Menu>(sql,new{ MenuDate = date });

        return menus.ToList();
    }

    public async Task<string> AddAsync(Menu menu)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = """
                    INSERT INTO menus (
                    menu_date, 
                    is_active, 
                    created_at, 
                    updated_at
                    ) 
                    VALUES (
                    @MenuDate, 
                    @IsActive 
                    @CreatedAt, 
                    @UpdatedAt
                    )
                  """;

        var result = await context.ExecuteAsync(sql, menu);
        return result == 0
            ? "Failed to add Menu."
            : "Menu added successfully.";
    }

    public async Task<string> UpdateAsync(int id, Menu menu)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT * FROM menus WHERE id = @Id";

            var menuExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
            if (menuExists != 1)
            {
                return "Menu not found.";
            }

            var sql2 = """
                       UPDATE menus
                       SET
                       menu_date=@MenuDate,
                       is_active=@IsActive, 
                       updated_at = NOW()
                       WHERE id = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                MenuDate = menu.MenuDate,
                IsActive = menu.IsActive,
                Updated_at = DateTime.Now
            });
            return result == 0
                ? "Failed to update Menu."
                : "Menu updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the Menu : {e.Message}";
        }
    }

    public async Task<string> DeleteAsync(int id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql1 = "SELECT * FROM menus WHERE id = @Id";

        var menuExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
        if (menuExists != 1)
        {
            return "Menu not found.";
        }

        var sql2 = "DELETE FROM menus WHERE id = @Id";
        var result = await context.ExecuteAsync(sql2, new { Id = id });
        return result == 0
            ? "Failed to delete Menu."
            : "Menu deleted successfully.";
    }

}

using Dapper;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class SubscriptionService:ISubscriptionService
{
    private readonly DataContext.DataContext _connection = new();


    public async Task<List<Subscription>> GetAllAsync()
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM Subscriptions";
        var Subscriptions = await context.QueryAsync<Subscription>(sql);

        return Subscriptions.ToList();
    }
    public async Task<List<Subscription>> GetByCompanyIdAsync(int company_id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM Subscriptions  WHERE company_id  = @Company_id";
        var Subscriptions = await context.QueryAsync<Subscription>(sql,company_id);

        return Subscriptions.ToList();
    }

    public async Task<string> AddAsync(Subscription subscription)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = """
                    INSERT INTO subscriptions (company_id,
                    plan_type,  
                    meals_per_day,
                    price, 
                    start_date,
                    end_date,
                    is_active,
                    created_at, 
                    updated_at
                    ) 
                    VALUES (
                    @CompanyId,
                    @PlanType,
                    @MealsPerDay,
                    @Price,
                    @StartDate,
                    @EndDate,
                    @IsActive, 
                    @CreatedAt,
                    @UpdatedAt
                    )
                  """;

        var result = await context.ExecuteAsync(sql, subscription);
        return result == 0
            ? "Failed to add Subscription."
            : "Subscription added successfully.";
    }

    public async Task<string> UpdateAsync(int id, Subscription subscription)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM subscriptions WHERE id = @Id";

            var subscriptionExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
            if (subscriptionExists != 1)
            {
                return "subscription not found.";
            }

            var sql2 = """
                       UPDATE subscriptions
                       SET
                       company_id = @CompanyId,
                        plan_type = @PlanType,  
                        meals_per_day = @MealsPerDay,
                        price = @Price, 
                        start_date =   @StartDate,
                        end_date =  @EndDate,
                        is_active = @IsActive,
                       updated_at  = @UpdatedAt
                       WHERE id  = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                CompanyId = subscription.CompanyId,
                PlanType = subscription.PlanType,
                MealsPerDay = subscription.MealsPerDay,
                Price = subscription.Price,
                CreatedAt = subscription.CreatedAt,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                IsActive = subscription.IsActive,
                UpdatedAt = DateTime.Now
            });
            return result == 0
                ? "Failed to update subscription."
                : "subscription updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the subscription : {e.Message}";
        }
    }
    public async Task<string> UpdateSubscriptionStatusAsync(int id,  bool isActive)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM subscriptions WHERE id = @Id ";

            var subscriptionExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id, isActive });
            if (subscriptionExists != 1)
            {
                return "subscription not found.";
            }

            var sql2 = """
                       UPDATE subscriptions
                       SET  
                       is_active = @Is_active,
                       updated_at  = NOW()
                       WHERE id  = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                Is_active = isActive,
                Updated_at = DateTime.Now
            });
            return result == 0
                ? "Failed to update subscription."
                : "subscription updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the subscription : {e.Message}";
        }
    }
    public async Task<string> DeleteAsync(int id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql1 = "SELECT 1 FROM subscriptions WHERE id = @Id";

        var subscriptionExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
        if (subscriptionExists != 1)
        {
            return "subscription not found.";
        }

        var sql2 = "DELETE FROM subscriptions WHERE id = @Id";
        var result = await context.ExecuteAsync(sql2, new { id });
        return result == 0
            ? "Failed to delete Subscription."
            : "Subscription deleted successfully.";
    }
}

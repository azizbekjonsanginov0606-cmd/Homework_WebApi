using Dapper;
using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.DataContext;
namespace Infrastructure.Services;

public class CompanyService : ICompanyService
{
    private readonly DataContext.DataContext _connection = new();


    public async Task<List<Company>> GetAllAsync()
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = "SELECT * FROM companies";
        var companys = await context.QueryAsync<Company>(sql);

        return companys.ToList();
    }
    public async Task<Company?> GetByIdAsync(int Id)
    {
        using var connection = _connection.GetConnection();

        const string sql = """
        SELECT *
        FROM companies
        WHERE id = @Id
    """;

        return await connection.QueryFirstOrDefaultAsync<Company>(sql, new { id=Id });
    }

    public async Task<string> AddAsync(Company company)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql = """
                    INSERT INTO companies (name,address, email, phone, created_at, updated_at) 
                                  VALUES (@Name,@Address, @Email, @Phone, @CreatedAt, @UpdatedAt)
                  """;

        var result = await context.ExecuteAsync(sql, new
            {
                Id =company.Id,
                Name = company.Name,
                Address = company.Address,
                Email = company.Email,
                Phone = company.Phone,
                CreatedAt=DateTime.Now,
                UpdatedAt = DateTime.Now
            });
        return result == 0
            ? "Failed to add Company."
            : "Company added successfully.";
    }

    public async Task<string> UpdateAsync(int id, Company company)
    {
        try
        {
            using var context = _connection.GetConnection();
            context.Open();

            var sql1 = "SELECT 1 FROM companies WHERE id = @Id";

            var companyExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
            if (companyExists != 1)
            {
                return "company not found.";
            }

            var sql2 = """
                       UPDATE companies
                       SET
                       name = @Name,
                       address = @Address,
                       email = @Email,
                       phone = @Phone,
                       updated_at = @UpdatedAt
                       WHERE id = @Id
                       """;

            var result = await context.ExecuteAsync(sql2, new
            {
                Id = id,
                Name = company.Name,
                Address = company.Address,
                Email = company.Email,
                Phone = company.Phone,
                UpdatedAt = DateTime.Now
            });
            return result == 0
                ? "Failed to update company."
                : "company updated successfully.";
        }
        catch (Exception e)
        {
            return $"An error occurred while updating the company : {e.Message}";
        }
    }

    public async Task<string> DeleteAsync(int id)
    {
        using var context = _connection.GetConnection();
        context.Open();
        var sql1 = "SELECT 1 FROM companies WHERE id = @Id";

        var companyExists = await context.QuerySingleOrDefaultAsync<int>(sql1, new { id });
        if (companyExists != 1)
        {
            return "company not found.";
        }

        var sql2 = "DELETE FROM companies WHERE id = @Id";
        var result = await context.ExecuteAsync(sql2, new { id });
        return result == 0
            ? "Failed to delete company."
            : "company deleted successfully.";
    }
}

using Dapper;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface ICompanyService
{
    Task<List<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int Id);
    Task<string> AddAsync(Company company);
    Task<string> UpdateAsync(int id, Company company);
    Task<string> DeleteAsync(int id);
}

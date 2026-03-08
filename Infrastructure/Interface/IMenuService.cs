using Dapper;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface IMenuService
{
    Task<List<Menu>> GetAllAsync();
    Task<string> AddAsync(Menu menu);
    Task<string> UpdateAsync(int id, Menu menu);
    Task<string> DeleteAsync(int id);
}

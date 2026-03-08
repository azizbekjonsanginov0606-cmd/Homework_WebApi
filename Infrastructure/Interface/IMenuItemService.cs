using Dapper;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface IMenuItemService
{
    Task<List<MenuItem>> GetAllAsync();
    Task<List<MenuItem>> GetByCategoryAsync(string category);
    Task<string> AddAsync(MenuItem menuItem);
    Task<string> UpdateAsync(int id, MenuItem menuItem);
    Task<string> DeleteAsync(int id);
}

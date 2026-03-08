using Dapper;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<List<Order>> GetCompanyOrdersAsync(int companyId);
    Task<string> AddAsync(Order order);
    Task<string> UpdateAsync(int id, Order order);
    Task<string> UpdateOrderStatusAsync(int id, string status);
    Task<string> DeleteAsync(int id);
}

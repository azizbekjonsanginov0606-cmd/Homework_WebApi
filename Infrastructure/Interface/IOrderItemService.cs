using Dapper;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface IOrderItemService
{
    Task<List<OrderItem>> GetAllAsync();
    Task<string> AddAsync(OrderItem orderItem);
    Task<string> UpdateAsync(int id, OrderItem orderItem);
    Task<string> DeleteAsync(int id);
}

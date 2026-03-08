using Dapper;
using Domain.Entities;
namespace Infrastructure.Interface;

public interface ISubscriptionService
{
    Task<List<Subscription>> GetAllAsync();
    Task<List<Subscription>> GetByCompanyIdAsync(int company_id);
    Task<string> AddAsync(Subscription subscription);
    Task<string> UpdateAsync(int id, Subscription subscription);
    Task<string> UpdateSubscriptionStatusAsync(int id, bool isActive);
    Task<string> DeleteAsync(int id);
}

using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/Subscriptions")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService = new SubscriptionService();

    [HttpGet]
    public async Task<List<Subscription>> GetAllAsync()
    {
       return await _subscriptionService.GetAllAsync();
    } 

    [HttpGet("{companyId:int}")]
    public async Task<List<Subscription>> GetByCompanyIdAsync(int company_id) => await _subscriptionService.GetByCompanyIdAsync(company_id);
    [HttpPost]
    public async Task<string> AddAsync(Subscription Subscription) => await _subscriptionService.AddAsync(Subscription);

    [HttpPut("{id:int}")]
    public async Task<string> UpdateAsync(int id, Subscription Subscription) => await _subscriptionService.UpdateAsync(id, Subscription);
    [HttpPut("{isActive:bool}")]
    public async Task<string> UpdateSubscriptionStatusAsync(int id, bool isActive) => await _subscriptionService.UpdateSubscriptionStatusAsync(id, isActive);

    [HttpDelete("{id:int}")]
    public async Task<string> DeleteAsync(int id) => await _subscriptionService.DeleteAsync(id);

}

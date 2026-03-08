using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/Orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService= new OrderService();
    
    [HttpGet]
    public async Task<List<Order>> GetAllAsync() => await _orderService.GetAllAsync();
    [HttpGet("{companyId}")]
    public async Task<List<Order>> GetCompanyOrdersAsync(int companyId)=> await _orderService.GetCompanyOrdersAsync(companyId);
    [HttpPost]
    public async Task<string> AddAsync(Order order) => await _orderService.AddAsync(order);

    [HttpPut("{id:int}")]
    public async Task<string> UpdateAsync(int id, Order order) => await _orderService.UpdateAsync(id, order);
    [HttpPut("{id}/status")]
    public async Task<string> UpdateOrderStatusAsync(int id, string status)=>await _orderService.UpdateOrderStatusAsync(id,status);

    [HttpDelete("{id:int}")]
    public async Task<string> DeleteAsync(int id) => await _orderService.DeleteAsync(id);

}

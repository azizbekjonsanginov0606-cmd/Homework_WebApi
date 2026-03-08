using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/OrderItems")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService= new OrderItemService();
    
    [HttpGet]
    public async Task<List<OrderItem>> GetAllAsync() => await _orderItemService.GetAllAsync();

    [HttpPost]
    public async Task<string> AddAsync(OrderItem OrderItem) => await _orderItemService.AddAsync(OrderItem);

    [HttpPut("{id:int}")]
    public async Task<string> UpdateAsync(int id, OrderItem OrderItem) => await _orderItemService.UpdateAsync(id, OrderItem);

    [HttpDelete("{id:int}")]
    public async Task<string> DeleteAsync(int id) => await _orderItemService.DeleteAsync(id);

}

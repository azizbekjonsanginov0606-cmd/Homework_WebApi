using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/MenuItems")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService = new MenuItemService();

    [HttpGet]
    public async Task<List<MenuItem>> GetAllAsync() => await _menuItemService.GetAllAsync();

    [HttpGet("{category}")]
    public async Task<List<MenuItem>> GetByCategoryAsync(string category) => await _menuItemService.GetByCategoryAsync(category);

    [HttpPost]
    public async Task<string> AddAsync(MenuItem menuItem) => await _menuItemService.AddAsync(menuItem);

    [HttpPut("{id:int}")]
    public async Task<string> UpdateAsync(int id, MenuItem menuItem) => await _menuItemService.UpdateAsync(id, menuItem);

    [HttpDelete("{id:int}")]
    public async Task<string> DeleteAsync(int id) => await _menuItemService.DeleteAsync(id);

}

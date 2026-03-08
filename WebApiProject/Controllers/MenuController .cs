using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/Menus")]
public class MenuController : ControllerBase
{
    private readonly IMenuService menuService= new MenuService();
    
    [HttpGet]
    public async Task<List<Menu>> GetAllAsync() => await menuService.GetAllAsync();

    [HttpPost]
    public async Task<string> AddAsync(Menu menu) => await menuService.AddAsync(menu);

    [HttpPut("{id:int}")]
    public async Task<string> UpdateAsync(int id, Menu menu) => await menuService.UpdateAsync(id, menu);

    [HttpDelete("{id:int}")]
    public async Task<string> DeleteAsync(int id) => await menuService.DeleteAsync(id);


}

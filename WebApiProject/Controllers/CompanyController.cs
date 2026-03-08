using Domain.Entities;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/Companys")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService companyService = new CompanyService();

    [HttpGet]
    public async Task<List<Company>> GetAllAsync() => await companyService.GetAllAsync();
    [HttpGet("{id:int}")]
    public async Task<Company?> GetByIdAsync(int Id) => await companyService.GetByIdAsync(Id);
    [HttpPost]
    public async Task<string> AddAsync(Company company) => await companyService.AddAsync(company);

    [HttpPut("{id:int}")]
    public async Task<string> UpdateAsync(int id, Company company) => await companyService.UpdateAsync(id, company);

    [HttpDelete("{id:int}")]
    public async Task<string> DeleteAsync(int id) => await companyService.DeleteAsync(id);

}

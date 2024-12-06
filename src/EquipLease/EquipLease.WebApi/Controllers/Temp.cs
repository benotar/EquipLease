using EquipLease.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class Temp : ControllerBase
{
    private readonly IDbContext _dbContext;

    public Temp(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        var temp = await _dbContext.ProductionFacilities.ToListAsync();

        return Ok(temp);
    }
}
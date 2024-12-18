using EquipLease.Application.Interfaces.DbContext;
using EquipLease.Application.Interfaces.Repository;
using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Persistence.Repository;

public class ProcessEquipmentTypeRepository : IProcessEquipmentTypeRepository
{
    private readonly IDbContext _dbContext;

    public ProcessEquipmentTypeRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProcessEquipmentType?> GetByCodeAsync(string code)
    {
        return await _dbContext.ProcessEquipmentTypes
            .FirstOrDefaultAsync(pet => pet.Code.Equals(code));
    }
}
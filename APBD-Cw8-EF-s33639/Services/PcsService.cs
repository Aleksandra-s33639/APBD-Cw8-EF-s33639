using APBD_Cw8_EF_s33639.Data;
using APBD_Cw8_EF_s33639.DTOs.Requests;
using APBD_Cw8_EF_s33639.DTOs.Responses;
using APBD_Cw8_EF_s33639.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Cw8_EF_s33639.Services;

public class PcsService : IPcsService
{
    private readonly AppDbContext _context;

    public PcsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcResponseDto>> GetAllAsync()
    {
        return await _context.PCs
            .Select(pc => new PcResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<PcComponentsResponseDto?> GetComponentsAsync(int id)
    {
        return await _context.PCs
            .Where(pc => pc.Id == id)
            .Select(pc => new PcComponentsResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PcComponents.Select(pcComponent => new PcComponentItemDto
                {
                    Amount = pcComponent.Amount,
                    Component = new ComponentDto
                    {
                        Code = pcComponent.Component.Code,
                        Name = pcComponent.Component.Name,
                        Description = pcComponent.Component.Description,
                        Manufacturer = new ComponentManufacturerDto
                        {
                            Id = pcComponent.Component.ComponentManufacturer.Id,
                            Abbreviation = pcComponent.Component.ComponentManufacturer.Abbreviation,
                            FullName = pcComponent.Component.ComponentManufacturer.FullName,
                            FoundationDate = pcComponent.Component.ComponentManufacturer.FoundationDate
                        },
                        Type = new ComponentTypeDto
                        {
                            Id = pcComponent.Component.ComponentType.Id,
                            Abbreviation = pcComponent.Component.ComponentType.Abbreviation,
                            Name = pcComponent.Component.ComponentType.Name
                        }
                    }
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PcResponseDto> CreateAsync(PcRequestDto request)
    {
        var pc = new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return MapToResponse(pc);
    }

    public async Task<PcResponseDto?> UpdateAsync(int id, PcRequestDto request)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(pc => pc.Id == id);

        if (pc is null)
        {
            return null;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return MapToResponse(pc);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(pc => pc.Id == id);

        if (pc is null)
        {
            return false;
        }

        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }

    private static PcResponseDto MapToResponse(Pc pc)
    {
        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }
}
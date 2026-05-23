using APBD_Cw8_EF_s33639.DTOs.Requests;
using APBD_Cw8_EF_s33639.DTOs.Responses;

namespace APBD_Cw8_EF_s33639.Services;

public interface IPcsService
{
    Task<List<PcResponseDto>> GetAllAsync();

    Task<PcComponentsResponseDto?> GetComponentsAsync(int id);

    Task<PcResponseDto> CreateAsync(PcRequestDto request);

    Task<PcResponseDto?> UpdateAsync(int id, PcRequestDto request);

    Task<bool> DeleteAsync(int id);
}
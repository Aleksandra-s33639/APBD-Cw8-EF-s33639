using System.ComponentModel.DataAnnotations;

namespace APBD_Cw8_EF_s33639.DTOs.Requests;

public class PcRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public double Weight { get; set; }

    [Required]
    public int Warranty { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public int Stock { get; set; }
}
namespace APBD_Cw8_EF_s33639.Models;

public class PcComponent
{
    public int PcId { get; set; }
    public string ComponentCode { get; set; } = null!;
    public int Amount { get; set; }
    public Pc Pc { get; set; } = null!;

    public Component Component { get; set; } = null!;
}
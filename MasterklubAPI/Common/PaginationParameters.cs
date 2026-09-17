using System.ComponentModel.DataAnnotations;

namespace MasterklubAPI.Common;

public class PaginationParameters
{
    [Range(1, int.MaxValue, ErrorMessage = "BrojStranice mora biti veći ili jednak 1.")]
    public int BrojStranice { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "VelicinaStranice mora biti između 1 i 100.")]
    public int VelicinaStranice { get; set; } = 10;

    public string? SortBy { get; set; }

    public bool SortOpadajuce { get; set; }
}

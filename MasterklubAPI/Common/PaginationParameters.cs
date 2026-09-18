namespace MasterklubAPI.Common;

public class PaginationParameters
{
    public int BrojStranice { get; set; } = 1;

    public int VelicinaStranice { get; set; } = 10;

    public string? SortBy { get; set; }

    public bool SortOpadajuce { get; set; }
}

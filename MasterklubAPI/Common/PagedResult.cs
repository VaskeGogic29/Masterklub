namespace MasterklubAPI.Common;

public record PagedResult<T>(
    IReadOnlyList<T> Stavke,
    int UkupanBrojElemenata,
    int BrojStranice,
    int VelicinaStranice)
{
    public int UkupanBrojStranica => (int)Math.Ceiling(UkupanBrojElemenata / (double)VelicinaStranice);
}

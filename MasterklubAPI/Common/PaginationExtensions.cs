using System.Linq.Expressions;
using System.Reflection;

namespace MasterklubAPI.Common;

public static class PaginationExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> source, PaginationParameters parametri)
    {
        var upit = source.AsQueryable();

        if (!string.IsNullOrWhiteSpace(parametri.SortBy))
        {
            var svojstvo = typeof(T).GetProperty(parametri.SortBy,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (svojstvo != null)
            {
                var parametarIzraza = Expression.Parameter(typeof(T), "x");
                var pristupSvojstvu = Expression.Property(parametarIzraza, svojstvo);
                var lambda = Expression.Lambda(pristupSvojstvu, parametarIzraza);

                var nazivMetode = parametri.SortOpadajuce ? "OrderByDescending" : "OrderBy";
                var pozivMetode = Expression.Call(
                    typeof(Queryable),
                    nazivMetode,
                    new[] { typeof(T), svojstvo.PropertyType },
                    upit.Expression,
                    Expression.Quote(lambda));

                upit = upit.Provider.CreateQuery<T>(pozivMetode);
            }
        }

        var ukupanBrojElemenata = upit.Count();

        var stavke = upit
            .Skip((parametri.BrojStranice - 1) * parametri.VelicinaStranice)
            .Take(parametri.VelicinaStranice)
            .ToList();

        return new PagedResult<T>(stavke, ukupanBrojElemenata, parametri.BrojStranice, parametri.VelicinaStranice);
    }
}

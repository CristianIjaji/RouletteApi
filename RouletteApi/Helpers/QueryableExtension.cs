using RouletteApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Helpers
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginateDTO paginateDTO)
        {
            return queryable
                .Skip((paginateDTO.Page - 1) * paginateDTO.RowsByPage)
                .Take(paginateDTO.RowsByPage);
        }
    }
}

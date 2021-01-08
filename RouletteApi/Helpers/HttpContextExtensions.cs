using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParams<T>(this HttpContext httpContext,
            IQueryable<T> queryble, int rowsByPage)
        {
            double cant = await queryble.CountAsync();
            double pages = Math.Ceiling(cant / rowsByPage);
            httpContext.Response.Headers.Add("pages", pages.ToString());
        }
    }
}

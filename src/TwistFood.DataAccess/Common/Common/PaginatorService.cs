using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Common.Interfaces;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Common.Common
{
    public class PaginatorService : IPaginatorService
    {
        private readonly HttpContextAccessor _accessor;

        public PaginatorService(HttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }
        public async Task<IList<T>> ToPagedAsync<T>(IQueryable<T> items, int pageNumber, int pageSize)
        {
            int totalitems = await items.CountAsync();
            PagenationMetaData pagenationMetaData = new PagenationMetaData()
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalitems,
                TotalPages = (int)Math.Ceiling((double)totalitems / pageSize),
                HasPrevious = pageNumber > 1,

            };
            pagenationMetaData.HasNext = pagenationMetaData.CurrentPage < pagenationMetaData.TotalPages;
            var json = JsonConvert.SerializeObject(pagenationMetaData, Formatting.Indented);
            _accessor.HttpContext.Response.Headers.Add("X-Pagination", "");

            return await items.Skip(pageNumber * pageSize - pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}

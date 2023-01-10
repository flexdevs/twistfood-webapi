using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Interfaces.Common;

namespace TwistFood.Service.Services.Common
{
    public class PaginatorService : IPaginatorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaginatorService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<T>> ToPageAsync<T>(IQueryable<T> items, int pageNumber, int pageSize)
        {
            int totalitems = await items.CountAsync();
            PagenationMetaData pagenationMetaData = new PagenationMetaData()
            {
                CurrentPage= pageNumber,
                PageSize= pageSize,
                TotalItems= totalitems,
                TotalPages= (int) Math.Ceiling((double)totalitems / (double)pageSize),
                HasPrevious = pageNumber > 1,
            };
            pagenationMetaData.HasNext = pagenationMetaData.CurrentPage < pagenationMetaData.TotalPages;
            string json = JsonConvert.SerializeObject(pagenationMetaData);
            _httpContextAccessor.HttpContext!.Response.Headers.Add("X-Pagination", json);

            return await items.Skip(pageNumber * pageSize - pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

    }
}

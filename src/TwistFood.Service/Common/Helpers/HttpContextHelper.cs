using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.Service.Common.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor { get; set; }
        public static HttpResponse Response => Accessor.HttpContext.Response;

        public static IHeaderDictionary ResponseHeaders => Response.Headers;

        public static HttpContext HttpContext => Accessor?.HttpContext;
        public static long UserId => GetUserId();

        private static long GetUserId()
        {
            long id;
            bool canParse = long.TryParse(HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value, out id);
            return canParse ? id : 0;
        }
        public static bool IsUser => IsUserOrAdmin();
        private static bool IsUserOrAdmin()
        {
            var claim = (HttpContext?.User?.Claims.FirstOrDefault(x => x.Type.Contains("role")));
            return claim is null ? true : false; 
        }
    }
}

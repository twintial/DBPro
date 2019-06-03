//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.新文件夹
{
    public class CookieHelper
    {
        public static void WriteCookie(HttpContext context, string strName, string strValue, double expires)
        {
            IResponseCookies cookie = context.Response.Cookies;
            if (cookie == null)
            {
                CookieOptions cp = new CookieOptions();
                cp.Expires = DateTimeOffset.Now.AddMinutes(expires);
                cookie.Append(strName, strValue, cp);
            }
 
        }

        public static string GetCookie(HttpContext context,string strName,string strVal)
        {
            string val;
            context.Request.Cookies.TryGetValue(strName, out val);

            return ((val ?? "")==strVal||strVal==null)?val:"";
        }

    }
}

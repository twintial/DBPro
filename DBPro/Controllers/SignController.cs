using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DBPro.Database;
using DBPro.Entity;
using DBPro.Models;
using DBPro.Parser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DBPro.Entity.User;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBPro.Controllers
{
    public class SignController : Controller
    {
        // GET: /<controller>/
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            
            return View();
        }
        public IActionResult SignInSucc()
        {
            return View();
        }


        [HttpOptions]
        [HttpPost]
        public async Task<string> SignInHandle()
        {
           using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p=ParseJson.parseSimpleParam(body);

                List<string> key = new List<string>(), val = new List<string>();
                key.Add("account");
                val.Add(p["account"]);
                key.Add("password");
                val.Add(p["password"]);
                //HttpContext.Response.Cookies =;
                if (DataBaseAccess.exist(typeof(Account), key, val))
                {
                    await testAsync(p["account"], p["password"]);
                    return "SignIn Successful!";
                }
                else
                    return "SignIn failed!";
                 
            }
        }



        [HttpOptions]
        [HttpPost]
        public string SignUpHandle(string ID, string password)
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                string id = getID();
                Account a = new Account(p["account"],id, p["password"], DateTime.Now, DateTime.Now, p["IP"]);
                User u = new User(id, null, null, normal, null);
                //people tem = new people(p["ID"], p["password"]);


                if (DataBaseAccess.insertObj(a)&&DataBaseAccess.insertObj(u))
                {
                    return "SignUp Successful!";
                }
                else
                    return "SignUp failed!";

            }
        }

        [HttpOptions]
        [HttpPost]
        public async Task<string> SignOutHandleAsync()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                List<string> key = new List<string>(), val = new List<string>();

                //HttpContext.Response.Cookies =;
                if (DataBaseAccess.exist(typeof(Account), key, val))
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    return "SignOut Successful!";
                }
                else
                    return "SignOut failed!";

            }
        }


        private string getID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            string str = guid.ToString();
            return str.Substring(0,15);
        }

        private async Task testAsync(string name,string id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier,id),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties()
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                RedirectUri = "Sign/SignIn"
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}

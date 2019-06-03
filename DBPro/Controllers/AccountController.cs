using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
using DBPro.Entity;
using DBPro.Parser;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBPro.Controllers
{
    public class AccountController : Controller
    {
        [HttpOptions]
        [HttpPost]
        public string AddReciverHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                UserInformation u = new UserInformation(p["rID"],p["ID"], p["name"], p["province"], p["city"], p["district"], p["street"], p["detailAdress"], p["phone"]);
                if (DataBaseAccess.insertObj(u))
                {
                    return "AddReciver Successful!";
                }
                else
                    return "AddReciver failed!";

                //return View();*/
            }
        }

        [HttpOptions]
        [HttpPost]
        public string DelReciverHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);
                
                UserInformation u = new UserInformation(p["rID"], p["ID"], p["name"], p["province"], p["city"], p["district"], p["street"], p["detailAdress"], p["phone"]);
                if (DataBaseAccess.deleteObj(u))
                {
                    return "DelReciver Successful!";
                }
                else
                    return "DelReciver failed!";

                //return View();*/
            }
        }
        [HttpOptions]
        [HttpPost]
        public string UpdateReciverHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                UserInformation u = new UserInformation(p["rID"], p["ID"], p["name"], p["province"], p["city"], p["district"], p["street"], p["detailAdress"], p["phone"]);
                if (DataBaseAccess.updateObj(u))
                {
                    return "DelReciver Successful!";
                }
                else
                    return "DelReciver failed!";

                //return View();*/
            }
        }

        [HttpOptions]
        [HttpPost]
        public string UpdateHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))


            {
                //var a = ConnectionFactory.CreateConnection(@"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME = orcl)));User Id=system;Password=990503");
                //a.Open();
                //OracleHelper.ExecuteSql(a, @"update DBUser set  userName ='123' , userGender ='0'  , userLevel = 1 , userIconID ='123124'  where  userID = '9770a066-cc46-4'");
                
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);
                List<string> key = new List<string>(), val = new List<string>();
                User u = new User(p["ID"], p["name"], p["gender"], 1, null);
                //HttpContext.Response.Cookies =;
                if (DataBaseAccess.updateObj(u))
                {
                    return "update Successful!";
                }
                else
                    return "update failed!";

                //return View();*/
            }
            
        }
    }
}

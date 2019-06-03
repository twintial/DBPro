using System;
using System.Collections.Generic;
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
    public class ItemController : Controller
    {
        [HttpOptions]
        [HttpPost]
        public string AddItemHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                Item u = new Item(p["i_ID"], p["s_ID"], p["name"], double.Parse(p["price"]), p["introduction"], int.Parse(p["followNum"]), double.Parse(p["score"]));
                if (DataBaseAccess.insertObj(u))
                {
                    return "AddItem Successful!";
                }
                else
                    return "AddItem failed!";

                //return View();*/
            }
        }

        [HttpOptions]
        [HttpPost]
        public string DelItemHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                Item u = new Item(p["i_ID"], p["s_ID"], p["name"], double.Parse(p["price"]), p["introduction"], int.Parse(p["followNum"]), double.Parse(p["score"]));
                if (DataBaseAccess.deleteObj(u))
                {
                    return "DelItem Successful!";
                }
                else
                    return "DelItem failed!";

                //return View();*/
            }
        }
        [HttpOptions]
        [HttpPost]
        public string UpdateItemHandle()
        {
            using (var reader = new StreamReader(HttpContext.Request.Body))
            {
                var body = reader.ReadToEnd();

                // Do something
                var p = ParseJson.parseSimpleParam(body);

                Item u = new Item(p["i_ID"], p["s_ID"], p["name"], double.Parse(p["price"]), p["introduction"], int.Parse(p["followNum"]), double.Parse(p["score"]));
                if (DataBaseAccess.updateObj(u))
                {
                    return "UpdateItem Successful!";
                }
                else
                    return "UpdateItem failed!";

                //return View();*/
            }
        }
    }
}

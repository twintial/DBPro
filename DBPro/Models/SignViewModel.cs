using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBPro.Models
{
    public class SignViewModel
    {
        [Required(ErrorMessage = "用户名不能为空。")]
        public string ID { set; get; }
        public string password { set; get; }
    }
}

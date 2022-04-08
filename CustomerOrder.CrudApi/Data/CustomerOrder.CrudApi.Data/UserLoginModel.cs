using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Data
{
    public class UserLoginModel
    {
        //Giriş işlemleri bunlar üzerinden olacak 
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

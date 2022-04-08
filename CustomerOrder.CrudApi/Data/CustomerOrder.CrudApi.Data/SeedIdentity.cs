using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Data
{
   public  class SeedIdentity
    {
        public static void Seed(UserManager<User> userManager)
        {
            
            //user nesnemi oluşturdum

            var user = new User()
            {
                UserName = "JwtUser",
                Email="jwtuser@hotmail.com"
                
            };

            //bu username veri tabanında yoksa
            if (userManager.FindByNameAsync("JwtUser").Result==null)

           {
                //ben oluşturdum yani userım oluşmuş oldu
                var identityResult = userManager.CreateAsync(user, "jwt123456").Result;
            }



        }

    }
}

using CustomerOrder.CrudApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Web.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public readonly IConfiguration _configuration;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        [HttpPost("login")]   //---api/controller/login dediğim zaman bu metod send olup bana bir token degerı gelecek

        public async Task <IActionResult> Login (UserLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //giriş yapacagım userım
            var user = await _userManager.FindByIdAsync(model.Username);
            if (user == null)

                return BadRequest(new { message = "email is incorrect" });

            //checkpas.. metodu ıle user ve passwordum kontrol edılıyor
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            //resultımın basarılı olup olmadıgını kontrol edıyorum 
            //başarılıysa ok donecek ve bu okeyın ıcınde bır token nesnesı olaccak
            //alltakı metodla beraber calısacak
            //basarılı olmazsa :unauthorized olacak

            return result.Succeeded ? Ok(new { token = GenerateJwtToken(user) }) : Unauthorized();

        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //Bu keyımle appsetting secrettekı keyımı okuyorum
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSetting:Secret").Value);


            //user ıdsı ve username ı ıstıyor benden
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)

                    }),

                Expires=DateTime.UtcNow.AddDays(1),

                //keyimi alıp buradaki 256 algortmasna gore olusturuyorum
                SigningCredentials=new SigningCredentials (new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                

            };

            //ve son olarak tokenımı olusturup döndürüyorum
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);

           
        }
    }
}

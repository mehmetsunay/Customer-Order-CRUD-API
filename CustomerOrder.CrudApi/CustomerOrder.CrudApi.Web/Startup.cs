
using CustomerOrder.Crud.Api.Services;
using CustomerOrder.CrudApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CustomerOrder.CrudApi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<CrudApiDbContext>();
            //identitymi crudapidbcontext �zerinde kullan�yorum
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CrudApiDbContext>().AddDefaultTokenProviders();
           
            //identitymi yap�land�rd�m bunu yapmazsam kafas�na gore g�der
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;


            });

            //as�l jwt y� burada yap�land�r�yorum 

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }) 
                 //jwtbearer yap�land�rmas�
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;

                    //token validation parameterlar�m� bel�rtt�m 
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true, //token de�erinin bu uygulmaya ait olup olmad���n� anlayan securitykey akt�flest�r�yor
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSetting:Secret").Value)), //olu�turulan token deger�m�z�n uyg. ait olup olmad�g�n� belirten bir security key.Buradak� appsett�ng json dosyam�zda
                        ValidateIssuer = false, // olu�turulan token de�erini kimin da��tt�g�n� �fade eden alan
                        ValidateAudience = false, // olu�turulan token de�erini kimler belirledi hangi siteler kullanacak



                    };
                });



             services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderServices, OrderService>();

         services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerOrder.CrudApi.Web", Version = "v1" });
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<User>userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerOrder.CrudApi.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          //SeedIdentity.Seed(userManager);

        }


    }
}

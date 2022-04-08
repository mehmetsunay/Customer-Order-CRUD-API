
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
            //identitymi crudapidbcontext üzerinde kullanýyorum
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CrudApiDbContext>().AddDefaultTokenProviders();
           
            //identitymi yapýlandýrdým bunu yapmazsam kafasýna gore gýder
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;


            });

            //asýl jwt yý burada yapýlandýrýyorum 

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }) 
                 //jwtbearer yapýlandýrmasý
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;

                    //token validation parameterlarýmý belýrttým 
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true, //token deðerinin bu uygulmaya ait olup olmadýðýný anlayan securitykey aktýflestýrýyor
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSetting:Secret").Value)), //oluþturulan token degerýmýzýn uyg. ait olup olmadýgýný belirten bir security key.Buradaký appsettýng json dosyamýzda
                        ValidateIssuer = false, // oluþturulan token deðerini kimin daðýttýgýný ýfade eden alan
                        ValidateAudience = false, // oluþturulan token deðerini kimler belirledi hangi siteler kullanacak



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

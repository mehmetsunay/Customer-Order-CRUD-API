using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//Reflection, runtime'da (çalışma anında) bir sınıfın, metodun veya property'nin
//üzerinde işlem yapmamızı sağlayan bir API (Application Programming Interface) dir
namespace CustomerOrder.CrudApi.Data
{
    public class CrudApiDbContext : IdentityDbContext<User>
    {
        protected readonly IConfiguration _configuration;
        

        //iki constructorım var


        public CrudApiDbContext()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\appsettings.json").Build();
        }

        //dependncy injection kullanıyorsak burası devreye gırecek
        public CrudApiDbContext([NotNull] IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected virtual IList<Assembly> Assemblies
        {
            get
            {
                return new List<Assembly>()
        {
            {
                Assembly.Load("CustomerOrder.CrudApi.Data")
            }
        };
            }
        }
        //Bu metod sayesinde veritabanı tabloları oluşturulmadan araya girecek, tablo isimlerine
        //müdahale edebilecek veya kolonlara istediğimiz ayarları gerçekleştirebileceğiz.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var assembly in Assemblies)
            {
                // Loads all types from an assembly which have an interface of IBase and is a public class
                var classes = assembly.GetTypes().Where(s => s.GetInterfaces().Any(_interface => _interface.Equals(typeof(IBase)) && s.IsClass && !s.IsAbstract && s.IsPublic));

                foreach (var _class in classes)
                {
                    // On Model Creating
                    var onModelCreatingMethod = _class.GetMethods().FirstOrDefault(x => x.Name == "OnModelCreating" && x.IsStatic);

                    if (onModelCreatingMethod != null)
                    {
                        //Invoke metodu zamana bağlı olarak fonksiyon çalıştırmaya yarar. 
                        onModelCreatingMethod.Invoke(_class, new object[] { builder });
                    }

                    // On Base Model Creating
                    if (_class.BaseType == null || _class.BaseType != typeof(Base))
                    {
                        continue;
                    }

                    var baseOnModelCreatingMethod = _class.BaseType.GetMethods().FirstOrDefault(x => x.Name == "OnModelCreating" && x.IsStatic);

                    if (baseOnModelCreatingMethod == null)
                    {
                        continue;
                    }

                    var baseOnModelCreatingGenericMethod = baseOnModelCreatingMethod.MakeGenericMethod(new Type[] { _class });

                    if (baseOnModelCreatingGenericMethod == null)
                    {
                        continue;
                    }

                    baseOnModelCreatingGenericMethod.Invoke(typeof(Base), new object[] { builder });
                }
           
            
            
             }
          
            builder.Entity<Customer>().HasKey(m => m.Name);
            base.OnModelCreating(builder);

        }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            // Sets the database connection from appsettings.json
            if (_configuration["ConnectionStrings:CrudApiDbContext"] != null)
            {
                builder.UseSqlServer(_configuration["ConnectionStrings:CrudApiDbContext"]);
            }
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is IBase)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("Created").CurrentValue = DateTimeOffset.Now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property("LastUpdated").CurrentValue = DateTimeOffset.Now;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }





    }
      





}

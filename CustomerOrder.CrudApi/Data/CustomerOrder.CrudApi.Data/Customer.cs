using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Data
{
    
    [Table("Customer",Schema="lge")]
    public class Customer : Base
    {
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Adress { get; set; }
       

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(property => property.Name).HasMaxLength(200);

            modelBuilder.Entity<Customer>().Property(property => property.Email).HasMaxLength(30);

            modelBuilder.Entity<Customer>().Property(property => property.Adress).HasMaxLength(100);
        }


    }
}

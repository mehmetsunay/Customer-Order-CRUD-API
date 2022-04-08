using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrder.CrudApi.Data
{

    [Table("Order", Schema = "lge")]
    public class Order : Base
    {
        public virtual string Product { get; set; }
        public virtual string Status { get; set; }
        public virtual int Price { get; set; }
        public virtual int Quantity { get; set; }
   

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(property => property.Product).HasMaxLength(200);

            modelBuilder.Entity<Order>().Property(property => property.Status).HasMaxLength(30);

            modelBuilder.Entity<Order>().Property(property => property.Price).HasMaxLength(100);

            modelBuilder.Entity<Order>().Property(property => property.Quantity).HasMaxLength(100);
        }






    }
}

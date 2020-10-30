using Microsoft.EntityFrameworkCore;
using System;

namespace Order.Entity.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) 
            : base(options)
        { }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = 1,
                OrderNumber = "20A123",
                OrderDate = DateTime.Now,
                OrderStatus = 1,
                PaymentType = 1,
                PaymentStatus = 1,
                SubTotal = 100000,
                Discount = 0,
                Total = 100000,
                FullName = "Lý Mạc Sầu",
                Email = "lymacsau@gmail.com",
                PhoneNumber = "0977123321",
                Address = "Cổ Mộ"
            },
            new Order
            {
                Id = 2,
                OrderNumber = "20A124",
                OrderDate = DateTime.Now,
                OrderStatus = 2,
                PaymentType = 1,
                PaymentStatus = 1,
                SubTotal = 200000,
                Discount = 10000,
                Total = 190000,
                FullName = "Tiểu Long Nữ",
                Email = "tieulongnu@gmail.com",
                PhoneNumber = "0977121121",
                Address = "Cổ Mộ"
            },
            new Order
            {
                Id = 3,
                OrderNumber = "20A125",
                OrderDate = DateTime.Now,
                OrderStatus = 4,
                PaymentType = 1,
                PaymentStatus = 2,
                SubTotal = 250000,
                Discount = 20000,
                Total = 230000,
                FullName = "Quái Cái",
                Email = "quaicai@gmail.com",
                PhoneNumber = "0977125521",
                Address = "Núi Hoàng Bạch"
            });
        }
    }
}

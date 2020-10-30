using System;
using System.Collections.Generic;

namespace Order.Entity
{
    public class Order : BaseEntity
    {
        // order information
        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }
        
        public int OrderStatus { get; set; }
        
        public int PaymentType { get; set; }

        public int PaymentStatus { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        // customer
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        // order items
        //public virtual List<OrderItem> Items { get; set; }
    }
}

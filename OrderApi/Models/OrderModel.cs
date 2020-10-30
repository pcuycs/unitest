using Order.Data;
using System;
using System.Collections.Generic;

namespace Order.Api.Models
{
    public class OrderModel
    {
        // order information
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentType PaymentType { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        // customer
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        // order items
        //public virtual List<OrderItemModel> Items { get; set; }
    }
}

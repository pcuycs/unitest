using Moq;
using Order.Data;
using Order.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Order.BusinessLogic.Test
{
    public partial class OrderBusinessTest
    {
        [Theory]
        [InlineData((int)OrderStatus.New, (int)PaymentStatus.NotPaid)]
        [InlineData((int)OrderStatus.New, (int)PaymentStatus.Paid)]
        public void CreateAsync_CreateOrderSuccess(int orderStatus, int paymentStatus)
        {
            // Arrange
            var order = new Entity.Order
            {
                Id = 1,
                OrderNumber = "20A123",
                OrderDate = DateTime.Now,
                OrderStatus = orderStatus,
                PaymentType = 1,
                PaymentStatus = paymentStatus,
                SubTotal = 100000,
                Discount = 0,
                Total = 100000,
                FullName = "Lý Mạc Sầu",
                Email = "lymacsau@gmail.com",
                PhoneNumber = "0977123321",
                Address = "Cổ Mộ"
            };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(a => a.SaveAsync(It.IsAny<OrderSaveRequest<Entity.Order>>()))
                .Returns(Task.FromResult(new OrderSaveResponse
                {
                    Id = order.Id,
                    Success = true
                }));

            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var response = orderBusiness.CreateAsync(new OrderSaveRequest<Entity.Order>
            {
                Entity = order,
                IsEdit = false
            }).GetAwaiter().GetResult();

            // Assert
            Assert.True(response.Success);
            Assert.Equal(1, response.Id);
        }

        [Theory]
        [InlineData(0, (int)PaymentStatus.NotPaid)]
        [InlineData(0, (int)PaymentStatus.Paid)]
        [InlineData((int)OrderStatus.New, 0)]
        [InlineData((int)OrderStatus.New, 3)]
        [InlineData((int)OrderStatus.Proccessing, (int)PaymentStatus.NotPaid)]
        [InlineData((int)OrderStatus.Proccessing, (int)PaymentStatus.Paid)]
        [InlineData((int)OrderStatus.Shipping, (int)PaymentStatus.NotPaid)]
        [InlineData((int)OrderStatus.Shipping, (int)PaymentStatus.Paid)]
        [InlineData((int)OrderStatus.Completed, (int)PaymentStatus.NotPaid)]
        [InlineData((int)OrderStatus.Completed, (int)PaymentStatus.Paid)]
        [InlineData((int)OrderStatus.Cancelled, (int)PaymentStatus.NotPaid)]
        [InlineData((int)OrderStatus.Cancelled, (int)PaymentStatus.Paid)]
        [InlineData(6, (int)PaymentStatus.NotPaid)]
        [InlineData(6, (int)PaymentStatus.Paid)]
        public void CreateAsync_CreateOrderFail(int orderStatus, int paymentStatus)
        {
            // Arrange
            var order = new Entity.Order
            {
                Id = 1,
                OrderNumber = "20A123",
                OrderDate = DateTime.Now,
                OrderStatus = orderStatus,
                PaymentType = 1,
                PaymentStatus = paymentStatus,
                SubTotal = 100000,
                Discount = 0,
                Total = 100000,
                FullName = "Lý Mạc Sầu",
                Email = "lymacsau@gmail.com",
                PhoneNumber = "0977123321",
                Address = "Cổ Mộ"
            };
            var request = new OrderSaveRequest<Entity.Order>
            {
                Entity = order,
                IsEdit = false
            };

            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(a => a.SaveAsync(request));

            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var response = orderBusiness.CreateAsync(new OrderSaveRequest<Entity.Order>
            {
                Entity = order,
                IsEdit = false
            }).GetAwaiter().GetResult();

            // Assert
            Assert.False(response.Success);
        }
    }
}

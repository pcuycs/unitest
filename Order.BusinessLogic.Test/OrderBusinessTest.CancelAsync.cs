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
        [InlineData((int)PaymentStatus.NotPaid, (int)OrderStatus.New)]
        [InlineData((int)PaymentStatus.NotPaid, (int)OrderStatus.Proccessing)]
        [InlineData((int)PaymentStatus.Paid, (int)OrderStatus.New)]
        [InlineData((int)PaymentStatus.Paid, (int)OrderStatus.Proccessing)]
        [InlineData((int)PaymentStatus.Paid, (int)OrderStatus.Shipping)]
        public void CancelAsync_CancelOrderSuccess(int paymentStatus, int orderStatus)
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
            mockOrderRepository.Setup(a => a.GetByOrderNumberAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(order));
            mockOrderRepository.Setup(a => a.SaveAsync(It.IsAny<OrderSaveRequest<Entity.Order>>()))
                .Returns(Task.FromResult(new OrderSaveResponse { Id = order.Id, Success = true }));

            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var response = orderBusiness.CancelAsync(order.OrderNumber).GetAwaiter().GetResult();

            // Assert
            Assert.True(response.Success);
            Assert.Equal(1, response.Id);
            Assert.Equal((int)OrderStatus.Cancelled, order.OrderStatus);
        }

        [Fact]
        public void CancelAsync_OrderNotFound()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(a => a.GetByOrderNumberAsync(It.IsAny<string>()));
            mockOrderRepository.Setup(a => a.SaveAsync(It.IsAny<OrderSaveRequest<Entity.Order>>()));

            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var response = orderBusiness.CancelAsync("12341234").GetAwaiter().GetResult();

            // Assert
            Assert.False(response.Success);
        }

        [Theory]
        [InlineData(0, (int)OrderStatus.New)]
        [InlineData(0, (int)OrderStatus.Proccessing)]
        [InlineData(0, (int)OrderStatus.Shipping)]
        [InlineData(0, (int)OrderStatus.Completed)]
        [InlineData(0, (int)OrderStatus.Cancelled)]
        [InlineData((int)PaymentStatus.NotPaid, (int)OrderStatus.Shipping)]
        [InlineData((int)PaymentStatus.NotPaid, (int)OrderStatus.Completed)]
        [InlineData((int)PaymentStatus.NotPaid, (int)OrderStatus.Cancelled)]
        [InlineData((int)PaymentStatus.Paid, (int)OrderStatus.Completed)]
        [InlineData((int)PaymentStatus.Paid, (int)OrderStatus.Cancelled)]
        [InlineData(3, (int)OrderStatus.New)]
        [InlineData(3, (int)OrderStatus.Proccessing)]
        [InlineData(3, (int)OrderStatus.Shipping)]
        [InlineData(3, (int)OrderStatus.Completed)]
        [InlineData(3, (int)OrderStatus.Cancelled)]
        public void CancelAsync_CancelOrderFail(int paymentStatus, int orderStatus)
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
            mockOrderRepository.Setup(a => a.GetByOrderNumberAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(order));
            mockOrderRepository.Setup(a => a.SaveAsync(It.IsAny<OrderSaveRequest<Entity.Order>>()))
                .Returns(Task.FromResult(new OrderSaveResponse { Id = order.Id, Success = true }));

            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var response = orderBusiness.CancelAsync(order.OrderNumber).GetAwaiter().GetResult();

            // Assert
            Assert.False(response.Success);
        }
    }
}

using Moq;
using Order.Data;
using Order.Repository;
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
        public void CanCancelOrder_Success(int paymentStatus, int orderStatus)
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var result = orderBusiness.CanCancelOrder(new Entity.Order
            {
                OrderStatus = orderStatus,
                PaymentStatus = paymentStatus
            });

            // Assert
            Assert.True(result);
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
        public void CanCancelOrder_Fail(int paymentStatus, int orderStatus)
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var result = orderBusiness.CanCancelOrder(new Entity.Order
            {
                OrderStatus = orderStatus,
                PaymentStatus = paymentStatus
            });

            // Assert
            Assert.False(result);
        }
    }
}

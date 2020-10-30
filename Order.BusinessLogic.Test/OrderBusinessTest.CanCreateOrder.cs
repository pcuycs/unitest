using Moq;
using Order.Data;
using Order.Repository;
using Xunit;

namespace Order.BusinessLogic.Test
{
    public partial class OrderBusinessTest
    {
        [Theory]
        [InlineData((int)OrderStatus.New, (int)PaymentStatus.NotPaid)]
        [InlineData((int)OrderStatus.New, (int)PaymentStatus.Paid)]
        public void CanCreateOrder_Success(int orderStatus, int paymentStatus)
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var result = orderBusiness.CanCreateOrder(new Entity.Order
            {
                OrderStatus = orderStatus,
                PaymentStatus = paymentStatus
            });

            // Assert
            Assert.True(result);
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
        public void CanCreateOrder_Fail(int orderStatus, int paymentStatus)
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var orderBusiness = new OrderBusiness(mockOrderRepository.Object);

            // Act
            var result = orderBusiness.CanCreateOrder(new Entity.Order
            {
                OrderStatus = orderStatus,
                PaymentStatus = paymentStatus
            });

            // Assert
            Assert.False(result);
        }
    }
}

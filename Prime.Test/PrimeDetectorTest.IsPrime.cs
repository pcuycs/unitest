using Xunit;

namespace Prime.Test
{
    public partial class PrimeDetectorTest
    {
        [Fact]
        public void IsPrime_IsAPrimeNumber()
        {
            // Arrange
            var primeDetector = new PrimeDetector();

            // Act
            var result = primeDetector.IsPrime(29);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPrime_IsNotAPrimeNumber()
        {
            // Arrange
            var primeDetector = new PrimeDetector();

            // Act
            var result = primeDetector.IsPrime(6);

            // Assert
            Assert.False(result);
        }



        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(11)]
        [InlineData(19)]
        [InlineData(23)]
        public void IsPrime_ArePrimeNumber(int number)
        {
            // Arrange
            var primeDetector = new PrimeDetector();

            // Act
            var result = primeDetector.IsPrime(number);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(12)]
        public void IsPrime_AreNotPrimeNumber(int number)
        {
            // Arrange
            var primeDetector = new PrimeDetector();

            // Act
            var result = primeDetector.IsPrime(number);

            // Assert
            Assert.False(result);
        }
    }
}

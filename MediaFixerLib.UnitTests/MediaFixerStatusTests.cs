using System;
using FluentAssertions;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
        public void ItemsTotal_MustBeGreaterThanOrEqualToZero()
        {
            // arrange
            const int itemsTotal = -1;

            // act/assert
            Assert.Throws<ArgumentOutOfRangeException>(() => MediaFixerStatus.Create(itemsTotal));
        }

        [Test]
        public void Create_WithoutMessage()
        {
            // arrange
            const int itemsTotal = 1;

            // act
            var status = MediaFixerStatus.Create(itemsTotal);

            // assert
            status.ItemsProcessed.Should().Be(0);
            status.ItemsTotal.Should().Be(itemsTotal);
            status.Message.Should().Be(string.Empty);
        }

        [Test]
        public void Create_WithMessage()
        {
            // arrange
            const int itemsTotal = 1;
            var message = Guid.NewGuid().ToString();

            // act
            var status = MediaFixerStatus.Create(itemsTotal, message);

            // assert
            status.ItemsProcessed.Should().Be(0);
            status.ItemsTotal.Should().Be(itemsTotal);
            status.Message.Should().Be(message);
        }

        [Test]
        public void ItemsProcessed_CanIncrement()
        {
            // arrange
            const int iterations = 3;
            var status = MediaFixerStatus.Create(default);

            // act
            for (var i = 0; i < iterations; i++) status.ItemProcessed();

            // assert
            status.ItemsProcessed.Should().Be(iterations);
        }
    }
}

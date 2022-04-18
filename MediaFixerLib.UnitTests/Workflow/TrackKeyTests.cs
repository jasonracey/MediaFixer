using FluentAssertions;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class TrackKeyTests
    {
        [TestCase(0, 0, "000-000")]
        [TestCase(1, 0, "001-000")]
        [TestCase(0, 1, "000-001")]
        [TestCase(1, 1, "001-001")]
        [TestCase(11, 0, "011-000")]
        [TestCase(0, 11, "000-011")]
        [TestCase(11, 11, "011-011")]
        [TestCase(111, 0, "111-000")]
        [TestCase(0, 111, "000-111")]
        [TestCase(111, 111, "111-111")]
        public void ReturnsExpectedKey(int discNumber, int trackNumber, string expectedKey)
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            mockTrack.SetupGet(t => t.DiscNumber).Returns(discNumber);
            mockTrack.SetupGet(t => t.TrackNumber).Returns(trackNumber);

            // act/assert
            mockTrack.Object.GetKey().Should().Be(expectedKey);
        }
    }
}

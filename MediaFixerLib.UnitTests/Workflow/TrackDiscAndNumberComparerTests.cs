using System.Collections.Generic;
using FluentAssertions;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class TrackDiscAndNumberComparerTests
    {
        private readonly IComparer<ITrack> _comparer = new TrackDiscAndNumberComparer();

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 1, 0, 0)]
        [TestCase(0, 1, 0, 1, 0)]
        [TestCase(1, 1, 1, 1, 0)]
        [TestCase(0, 0, 0, 1, -1)]
        [TestCase(0, 0, 1, 0, -1)]
        [TestCase(0, 1, 1, 1, -1)]
        [TestCase(1, 0, 1, 1, -1)]
        [TestCase(1, 0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0, 1)]
        [TestCase(1, 1, 1, 0, 1)]
        [TestCase(1, 1, 0, 1, 1)]
        public void WhenTracksAreEqual_ReturnsEqual(
            int discNumber1,
            int trackNumber1,
            int discNumber2,
            int trackNumber2,
            int expectedResult)
        {
            // arrange
            var mockTrack1 = new Mock<ITrack>();
            mockTrack1.SetupGet(t => t.DiscNumber).Returns(discNumber1);
            mockTrack1.SetupGet(t => t.TrackNumber).Returns(trackNumber1);

            var mockTrack2 = new Mock<ITrack>();
            mockTrack2.SetupGet(t => t.DiscNumber).Returns(discNumber2);
            mockTrack2.SetupGet(t => t.TrackNumber).Returns(trackNumber2);

            // act
            var result = _comparer.Compare(mockTrack1.Object, mockTrack2.Object);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}

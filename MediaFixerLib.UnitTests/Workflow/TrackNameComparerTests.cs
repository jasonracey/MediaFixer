using System.Collections.Generic;
using FluentAssertions;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class TrackNameComparerTests
    {
        private readonly IComparer<ITrack> _comparer = new TrackNameComparer();

        [TestCase(null, null, 0)]
        [TestCase("", "", 0)]
        [TestCase(" ", " ", 0)]
        [TestCase("a", "a", 0)]
        [TestCase("a", "b", -1)]
        [TestCase("b", "a", 1)]
        public void WhenTracksAreEqual_ReturnsEqual(
            string trackName1,
            string trackName2,
            int expectedResult)
        {
            // arrange
            var mockTrack1 = new Mock<ITrack>();
            mockTrack1.SetupGet(t => t.TrackName).Returns(trackName1);

            var mockTrack2 = new Mock<ITrack>();
            mockTrack2.SetupGet(t => t.TrackName).Returns(trackName2);

            // act
            var result = _comparer.Compare(mockTrack1.Object, mockTrack2.Object);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}

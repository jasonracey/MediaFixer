using System.Collections.Generic;
using FluentAssertions;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class TrackComparerFactoryTests
    {
        private static Mock<ITrack> GetMockTrack(int trackNumber)
        {
            var track = new Mock<ITrack>();
            track.SetupGet(mock => mock.TrackNumber).Returns(trackNumber);
            return track;
        }

        [Test]
        public void WhenAnyTrackNumberIsZero_ReturnsTrackNameComparer()
        {
            // arrange
            var tracks = new List<ITrack>
            {
                GetMockTrack(0).Object
            };

            // act
            var comparer = TrackComparerFactory.GetTrackComparer(tracks);

            // assert
            comparer.Should().BeOfType<TrackNameComparer>();
        }

        [Test]
        public void WhenAnyTrackNumbersAreTheSame_ReturnsTrackNameComparer()
        {
            // arrange
            var tracks = new List<ITrack>
            {
                GetMockTrack(1).Object,
                GetMockTrack(1).Object
            };

            // act
            var comparer = TrackComparerFactory.GetTrackComparer(tracks);

            // assert
            comparer.Should().BeOfType<TrackNameComparer>();
        }

        [Test]
        public void WhenNoTrackNumberIsZero_AndNoTrackNumbersAreTheSame_ReturnsTrackDiscAndNumberComparer()
        {
            // arrange
            var tracks = new List<ITrack>
            {
                GetMockTrack(9).Object,
                GetMockTrack(3).Object,
                GetMockTrack(7).Object,
            };

            // act
            var comparer = TrackComparerFactory.GetTrackComparer(tracks);

            // assert
            comparer.Should().BeOfType<TrackDiscAndNumberComparer>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Fixer;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class TrackCountFixerTests
    {
        [Test]
        public void WhenTracksIsNull_ThrowsExpectedException()
        {
            // act/assert
            Assert.Throws<ArgumentNullException>(() => TrackCountFixer.FixTrackCounts(null!));
        }

        [Test]
        public void WhenTracksIsEmpty_DoesNotThrow()
        {
            // arrange
            // ReSharper disable once CollectionNeverUpdated.Local
            var tracks = new List<ITrack>();

            // act/assert
            TrackCountFixer.FixTrackCounts(tracks);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void SetsExpectedTrackCount(int trackCount)
        {
            // arrange
            var tracks = TestData.BuildMockAlbum(trackCount).ToList();

            // act
            TrackCountFixer.FixTrackCounts(tracks);

            // assert
            Assert.IsTrue(tracks.All(t => t.TrackCount == trackCount));
        }
    }
}

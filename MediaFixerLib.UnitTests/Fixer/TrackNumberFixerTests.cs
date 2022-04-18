using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Fixer;
using MediaFixerLib.Workflow;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Fixer
{
    [TestFixture]
    public class TrackNumberFixerTests
    {
        [Test]
        public void WhenTracksIsNull_ThrowsExpectedException()
        {
            // act/assert
            Assert.Throws<ArgumentNullException>(() => TrackNumberFixer.FixTrackNumbers(null!));
        }

        [Test]
        public void WhenTracksIsEmpty_DoesNotThrow()
        {
            // arrange
            // ReSharper disable once CollectionNeverUpdated.Local
            var tracks = new List<ITrack>();

            // act/assert
            TrackNumberFixer.FixTrackNumbers(tracks);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void SetsSequentialTrackNumbers(int trackCount)
        {
            // arrange
            var tracks = TestData.BuildMockAlbum(trackCount).ToList();

            // act
            TrackNumberFixer.FixTrackNumbers(tracks);

            // assert
            var trackList = tracks.ToList();
            trackList.Sort(new TrackDiscAndNumberComparer());
            for (var i = 0; i < trackList.Count; i++)
            {
                Assert.AreEqual(i + 1, trackList[i].TrackNumber);
            }
        }

        [Test]
        public void CanSortTracksByName()
        {
            // arrange
            var tracks = TestData.BuildMockAlbum(trackCount: 10, setupTrackNumbers: false).ToList();

            // act
            TrackNumberFixer.FixTrackNumbers(tracks);

            // assert
            for (var i = 0; i < tracks.Count - 2; i++)
            {
                var curr = tracks.Single(t => t.TrackNumber == i + 1);
                var next = tracks.Single(t => t.TrackNumber == i + 2);
                Assert.AreEqual(-1, curr.TrackName?.CompareTo(next.TrackName));
            }
        }
    }
}

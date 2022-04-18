using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class AlbumBuilderTests
    {
        [Test]
        public void WhenTracksIsNull_Throws()
        {
            // arrange
            var status = MediaFixerStatus.Create(0);

            // act/assert
            Assert.Throws<ArgumentNullException>(() => AlbumBuilder.BuildAlbums(null!, ref status));
        }

        [Test]
        public void WhenTracksToFixIsEmpty_ReturnsExpectedResult_AndStatus()
        {
            // arrange
            var status = MediaFixerStatus.Create(0);
            var tracksToFix = new List<ITrack>();

            // act
            var result = AlbumBuilder.BuildAlbums(tracksToFix, ref status);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
            Assert.AreEqual(0, status.ItemsTotal);
            Assert.AreEqual(0, status.ItemsProcessed);
            Assert.AreEqual("Generating album list...", status.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void WhenAlbumNameIsNullOrWhiteSpace_Throws(string albumName)
        {
            // arrange
            var status = MediaFixerStatus.Create(0);
            var mockTrack = new Mock<ITrack>();
            mockTrack.Setup(t => t.AlbumName).Returns(albumName);
            var tracksToFix = new List<ITrack> { mockTrack.Object };

            // act/assert
            Assert.Throws<MediaFixerException>(() => AlbumBuilder.BuildAlbums(tracksToFix, ref status));
        }

        [Test]
        public void WhenTracksToFixIsNotEmpty_GroupsTracksByAlbumName()
        {
            // arrange
            const int albumCount = 7;
            var status = MediaFixerStatus.Create(0);
            var tracksToFix = TestData.BuildMockAlbums(albumCount).SelectMany(t => t).ToArray();

            // act
            var result = AlbumBuilder.BuildAlbums(tracksToFix, ref status);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(albumCount, result.Count);
            Assert.AreEqual(tracksToFix.Length, status.ItemsTotal);
            Assert.AreEqual(tracksToFix.Length, status.ItemsProcessed);
            Assert.AreEqual("Generating album list...", status.Message);
        }
    }
}

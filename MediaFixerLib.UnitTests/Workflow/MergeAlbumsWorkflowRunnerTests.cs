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
    public class MergeAlbumsWorkflowRunnerTests
    {
        private IList<ITrack> _testTracks = new List<ITrack>();
        private Mock<IWorkflowRunnerInfo> _mockWorkflowData = new();
        private MediaFixerStatus _status = MediaFixerStatus.Create(0);
        private MergeAlbumsWorkflowRunner _mergeAlbumsWorkflowRunner = new();

        [SetUp]
        public void SetUp()
        {
            _mockWorkflowData = new Mock<IWorkflowRunnerInfo>();
            _status = MediaFixerStatus.Create(default);
            _mergeAlbumsWorkflowRunner = new MergeAlbumsWorkflowRunner();
        }

        [Test]
        public void WhenWorkflowInfoNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _mergeAlbumsWorkflowRunner.Run(null!, ref _status));
        }

        [Test]
        public void WhenTracksNull_Throws()
        {
            // arrange
            _mockWorkflowData.Setup(m => m.Tracks).Returns((IList<ITrack>)null!);

            // act/assert
            Assert.Throws<ArgumentException>(() => _mergeAlbumsWorkflowRunner.Run(_mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenTracksIsEmpty_DoesNotThrow()
        {
            // arrange
            _mockWorkflowData.Setup(m => m.Tracks).Returns(new List<ITrack>());

            // act
            _mergeAlbumsWorkflowRunner.Run(_mockWorkflowData.Object, ref _status);

            // assert
            Assert.AreEqual(0, _status.ItemsProcessed);
            Assert.AreEqual(0, _status.ItemsTotal);
            Assert.AreEqual("Running merge albums workflow...", _status.Message);
        }

        [Test]
        public void WhenTracksIsNotEmpty_CanMergeSingleMultiDiscAlbums()
        {
            // arrange
            const int discCount = 2;
            const string albumName = "Mock Album";
            var albums = TestData.BuildMockMultiDiscAlbum(discCount, albumName);
            _testTracks = albums.SelectMany(album => album).ToArray();
            _mockWorkflowData.Setup(m => m.Tracks).Returns(_testTracks);

            // act
            _mergeAlbumsWorkflowRunner.Run(_mockWorkflowData.Object, ref _status);

            // assert
            Assert.AreEqual(1, _status.ItemsProcessed);
            Assert.AreEqual(1, _status.ItemsTotal);
            Assert.AreEqual("Running merge albums workflow...", _status.Message);

            var processedTracks = _mockWorkflowData.Object.Tracks.ToArray();

            Assert.IsTrue(processedTracks.All(track => track.AlbumName == albumName));
            Assert.IsTrue(processedTracks.All(track => track.DiscCount == 1));
            Assert.IsTrue(processedTracks.All(track => track.DiscNumber == 1));
            Assert.IsTrue(processedTracks.All(track => track.TrackCount == _testTracks.Count()));
            AssertTrackNumbersAreCorrect(processedTracks, _testTracks.Count);
        }

        [Test]
        public void WhenTracksIsNotEmpty_CanMergeMultipleMultiDiscAlbums()
        {
            // arrange
            const int firstDiscCount = 4;
            const string firstAlbumName = "First Mock Album";
            var firstSetOfDiscs = TestData
                .BuildMockMultiDiscAlbum(firstDiscCount, firstAlbumName)
                .ToArray();

            const int secondDiscCount = 3;
            const string secondAlbumName = "Second Mock Album";
            var secondSetOfDiscs = TestData
                .BuildMockMultiDiscAlbum(secondDiscCount, secondAlbumName)
                .ToArray();

            _testTracks = firstSetOfDiscs.Concat(secondSetOfDiscs).SelectMany(album => album).ToArray();
            _mockWorkflowData.Setup(m => m.Tracks).Returns(_testTracks);

            // act
            _mergeAlbumsWorkflowRunner.Run(_mockWorkflowData.Object, ref _status);

            // assert
            Assert.AreEqual(2, _status.ItemsProcessed);
            Assert.AreEqual(2, _status.ItemsTotal);
            Assert.AreEqual("Running merge albums workflow...", _status.Message);

            var processedTracks = _mockWorkflowData.Object.Tracks.ToArray();

            Assert.IsTrue(processedTracks.All(track => track.DiscCount == 1));
            Assert.IsTrue(processedTracks.All(track => track.DiscNumber == 1));

            var expectedFirstAlbumTrackCount = firstSetOfDiscs.SelectMany(track => track).Count();
            var firstAlbumTracks = processedTracks.Where(track => track.AlbumName == firstAlbumName).ToArray();
            Assert.AreEqual(expectedFirstAlbumTrackCount, firstAlbumTracks.Length);
            Assert.IsTrue(firstAlbumTracks.All(track => track.TrackCount == expectedFirstAlbumTrackCount));
            AssertTrackNumbersAreCorrect(firstAlbumTracks, expectedFirstAlbumTrackCount);

            var expectedSecondAlbumTrackCount = secondSetOfDiscs.SelectMany(track => track).Count();
            var secondAlbumTracks = processedTracks.Where(track => track.AlbumName == secondAlbumName).ToArray();
            Assert.AreEqual(expectedSecondAlbumTrackCount, secondAlbumTracks.Length);
            Assert.IsTrue(secondAlbumTracks.All(track => track.TrackCount == expectedSecondAlbumTrackCount));
            AssertTrackNumbersAreCorrect(secondAlbumTracks, expectedSecondAlbumTrackCount);
        }

        private static void AssertTrackNumbersAreCorrect(IEnumerable<ITrack> tracks, int expectedTrackCount)
        {
            var trackArray = tracks.ToArray();
            for (var i = 0; i < expectedTrackCount; i++)
            {
                Assert.IsNotNull(trackArray.FirstOrDefault(track => track.TrackNumber == i + 1));
            }
        }
    }
}
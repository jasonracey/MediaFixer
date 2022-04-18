using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class AlbumWorkflowRunnerTests
    {
        private readonly int _albumCount = TestData.Random.Next(1, 10);

        private IList<ITrack> _tracksToFix = new List<ITrack>();
        private IList<MediaFixerLib.Workflow.Workflow> _workflows = new List<MediaFixerLib.Workflow.Workflow>();

        private WorkflowRunnerInfo _workflowRunnerInfo = new(new List<ITrack>());
        private MediaFixerStatus _status = MediaFixerStatus.Create(0);

        private AlbumWorkflowRunner _albumWorkflowRunner = new();

        [SetUp]
        public void SetUp()
        {
            _tracksToFix = TestData.BuildMockAlbums(_albumCount).SelectMany(t => t).ToArray();
            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.FixTrackNumbers),
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.FixCountOfTracksOnAlbum)
            };

            _workflowRunnerInfo = new WorkflowRunnerInfo(_tracksToFix, _workflows);
            _status = MediaFixerStatus.Create(default);

            _albumWorkflowRunner = new AlbumWorkflowRunner();
        }

        [Test]
        public void WhenWorkflowInfoNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _albumWorkflowRunner.Run(null!, ref _status));
        }

        [Test]
        public void WhenTracksNull_Throws()
        {
            var mockWorkflowData = new Mock<IWorkflowRunnerInfo>();
            mockWorkflowData.Setup(m => m.Tracks).Returns((IList<ITrack>)null!);
            Assert.Throws<ArgumentException>(() => _albumWorkflowRunner.Run(mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenWorkflowsNull_Throws()
        {
            var mockWorkflowData = new Mock<IWorkflowRunnerInfo>();
            mockWorkflowData.Setup(m => m.Tracks).Returns(new List<ITrack>());
            mockWorkflowData.Setup(m => m.Workflows).Returns((IEnumerable<MediaFixerLib.Workflow.Workflow>)null!);
            Assert.Throws<ArgumentException>(() => _albumWorkflowRunner.Run(mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenNoTracks_SetsStatus_AndReturns()
        {
            // arrange
            _tracksToFix = new List<ITrack>();
            _workflowRunnerInfo = new WorkflowRunnerInfo(_tracksToFix, _workflows);

            // act
            _albumWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            _status.Should().NotBeNull();
            _status.ItemsProcessed.Should().Be(0);
            _status.ItemsTotal.Should().Be(0);
            _status.Message.Should().Be("Running album workflows...");
        }

        [Test]
        public void WhenNoWorkflows_SetsStatus_AndReturns()
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            mockTrack.SetupGet(t => t.AlbumName).Returns(Guid.NewGuid().ToString());
            _tracksToFix = new List<ITrack> { mockTrack.Object };
            _workflows = new List<MediaFixerLib.Workflow.Workflow>();
            _workflowRunnerInfo = new WorkflowRunnerInfo(_tracksToFix, _workflows);

            // act
            _albumWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            _status.Should().NotBeNull();
            _status.ItemsProcessed.Should().Be(_tracksToFix.Count);
            _status.ItemsTotal.Should().Be(_tracksToFix.Count);
            _status.Message.Should().Be("Running album workflows...");
            mockTrack.VerifySet(t => t.TrackCount = It.IsAny<int>(), Times.Never);
            mockTrack.VerifySet(t => t.TrackNumber = It.IsAny<int>(), Times.Never);
        }

        [Test]
        public void FixesNumbersBeforeCounts()
        {
            // act
            _albumWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            _status.Should().NotBeNull();
            _status.ItemsProcessed.Should().Be(_albumCount);
            _status.ItemsTotal.Should().Be(_albumCount);
            _status.Message.Should().Be("Running album workflows...");
            foreach (var album in _workflowRunnerInfo.Tracks.GroupBy(t => t.AlbumName))
            {
                foreach (var track in album)
                {
                    track.TrackCount.Should().Be(album.Count());
                }
            }
        }
    }
}

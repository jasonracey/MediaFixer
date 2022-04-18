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
    public class TrackWorkflowRunnerTests
    {
        private IList<ITrack> _tracksToFix = new List<ITrack>();
        private IList<MediaFixerLib.Workflow.Workflow> _workflows = new List<MediaFixerLib.Workflow.Workflow>();

        private WorkflowRunnerInfo _workflowRunnerInfo = new(new List<ITrack>());
        private MediaFixerStatus _status = MediaFixerStatus.Create(0);

        private TrackWorkflowRunner _trackWorkflowRunner = new();

        [SetUp]
        public void SetUp()
        {
            _tracksToFix = new List<ITrack>();
            _workflows = new List<MediaFixerLib.Workflow.Workflow>();

            _workflowRunnerInfo = new WorkflowRunnerInfo(_tracksToFix, _workflows);
            _status = MediaFixerStatus.Create(default);

            _trackWorkflowRunner = new TrackWorkflowRunner();
        }

        [Test]
        public void WhenWorkflowDataNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _trackWorkflowRunner.Run(null!, ref _status));
        }

        [Test]
        public void WhenTracksNull_Throws()
        {
            var mockWorkflowData = new Mock<IWorkflowRunnerInfo>();
            mockWorkflowData.Setup(m => m.Tracks).Returns((IList<ITrack>)null!);
            Assert.Throws<ArgumentException>(() => _trackWorkflowRunner.Run(mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenWorkflowsNull_Throws()
        {
            var mockWorkflowData = new Mock<IWorkflowRunnerInfo>();
            mockWorkflowData.Setup(m => m.Tracks).Returns(new List<ITrack>());
            mockWorkflowData.Setup(m => m.Workflows).Returns((IEnumerable<MediaFixerLib.Workflow.Workflow>)null!);
            Assert.Throws<ArgumentException>(() => _trackWorkflowRunner.Run(mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenNoTracks_SetsStatus_AndReturns()
        {
            // arrange
            _tracksToFix = new List<ITrack>();

            // act
            _trackWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            _status.Should().NotBeNull();
            _status.ItemsProcessed.Should().Be(0);
            _status.ItemsTotal.Should().Be(0);
            _status.Message.Should().Be("Running track workflows...");
        }

        [Test]
        public void WhenNoWorkflows_SetsStatus_AndReturns()
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            _tracksToFix.Add(mockTrack.Object);
            _workflowRunnerInfo = new WorkflowRunnerInfo(_tracksToFix, _workflows);

            // act
            _trackWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            _status.Should().NotBeNull();
            _status.ItemsProcessed.Should().Be(_tracksToFix.Count());
            _status.ItemsTotal.Should().Be(_tracksToFix.Count());
            _status.Message.Should().Be("Running track workflows...");
            mockTrack.VerifySet(t => t.AlbumName = It.IsAny<string>(), Times.Never);
            mockTrack.VerifySet(t => t.Comment = It.IsAny<string>(), Times.Never);
            mockTrack.VerifySet(t => t.TrackName = It.IsAny<string>(), Times.Never);
        }
        
        [TestCase("t1", "a1", 0)]
        [TestCase("t1", null, 1)]
        [TestCase("t1", "", 1)]
        [TestCase("t1", " ", 1)]
        public void WhenSetAlbumNames_IfAlbumNameNullOrWhiteSpace_SetsToTrackName(string trackName, string albumName, int times)
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            mockTrack.Setup(t => t.TrackName).Returns(trackName);
            mockTrack.Setup(t => t.AlbumName).Returns(albumName);
            _tracksToFix.Add(mockTrack.Object);
            _workflows.Add(MediaFixerLib.Workflow.Workflow.Create(name: WorkflowName.SetAlbumNames));

            // act
            _trackWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            mockTrack.VerifySet(t => t.AlbumName = trackName, Times.Exactly(times));
        }
        
        [TestCase("x", "y", 1)]
        [TestCase(null, "y", 0)]
        [TestCase("", "y", 0)]
        public void WhenFindAndReplace_IfOldValueNotNullOrEmpty_SetsNewValue(string oldValue, string newValue, int times)
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            mockTrack.Setup(t => t.TrackName).Returns(Guid.NewGuid().ToString());
            _tracksToFix.Add(mockTrack.Object);
            _workflows.Add(MediaFixerLib.Workflow.Workflow.Create(name: WorkflowName.FindAndReplace, oldValue: oldValue, newValue: newValue));

            // act
            _trackWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            mockTrack.VerifySet(t => t.TrackName = It.IsAny<string>(), Times.Exactly(times));
        }

        [Test]
        public void WhenFixTrackName_SetsTrackName()
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            mockTrack.Setup(t => t.TrackName).Returns(Guid.NewGuid().ToString());
            _tracksToFix.Add(mockTrack.Object);
            _workflows.Add(MediaFixerLib.Workflow.Workflow.Create(name: WorkflowName.FixTrackNames));

            // act
            _trackWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            mockTrack.VerifySet(t => t.TrackName = It.IsAny<string>(), Times.Once);
        }

        [Test]
        public void WhenFixGratefulDeadTracks_AndCommentIsNotNullOrWhiteSpace_SetsName_AndComment()
        {
            // arrange
            var expectedComment = Guid.NewGuid().ToString();
            var mockTrack = new Mock<ITrack>();
            mockTrack.Setup(t => t.TrackName).Returns(Guid.NewGuid().ToString());
            mockTrack.Setup(t => t.Comment).Returns("https://archive.org/details/" + expectedComment);
            _tracksToFix.Add(mockTrack.Object);
            _workflows.Add(MediaFixerLib.Workflow.Workflow.Create(name: WorkflowName.FixGratefulDeadTracks));

            // act
            _trackWorkflowRunner.Run(_workflowRunnerInfo, ref _status);

            // assert
            mockTrack.VerifySet(t => t.TrackName = It.IsAny<string>(), Times.Once);
            mockTrack.VerifySet(t => t.Comment = expectedComment, Times.Once);
        }
    }
}

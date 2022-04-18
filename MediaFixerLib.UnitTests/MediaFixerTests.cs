using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests
{
    [TestFixture]
    public class MediaFixerTests
    {
        private static Mock<IWorkflowRunner> _mockMergeAlbumsWorkflowRunner = new();
        private static Mock<IWorkflowRunner> _mockImportTrackNamesWorkflowRunner = new();
        private static Mock<IWorkflowRunner> _mockAlbumWorkflowRunner = new();
        private static Mock<IWorkflowRunner> _mockTrackWorkflowRunner = new();
        
        private static MediaFixer _target = new(
            _mockMergeAlbumsWorkflowRunner.Object,
            _mockImportTrackNamesWorkflowRunner.Object,
            _mockAlbumWorkflowRunner.Object,
            _mockTrackWorkflowRunner.Object);

        private IEnumerable<ITrack> _tracks = new List<ITrack>();
        private IEnumerable<MediaFixerLib.Workflow.Workflow> _workflows = new List<MediaFixerLib.Workflow.Workflow>();

        private static void VerifyRun(Mock<IWorkflowRunner> mockWorkflowRunner, Times times)
        {
            mockWorkflowRunner.Verify(mock => mock.Run(
                It.IsAny<WorkflowRunnerInfo>(),
                ref It.Ref<MediaFixerStatus>.IsAny), times);
        }

        [SetUp]
        public void SetUp()
        {
            _mockMergeAlbumsWorkflowRunner = new Mock<IWorkflowRunner>();
            _mockImportTrackNamesWorkflowRunner = new Mock<IWorkflowRunner>();
            _mockAlbumWorkflowRunner = new Mock<IWorkflowRunner>();
            _mockTrackWorkflowRunner = new Mock<IWorkflowRunner>();
        
            _target = new MediaFixer(
                _mockMergeAlbumsWorkflowRunner.Object,
                _mockImportTrackNamesWorkflowRunner.Object,
                _mockAlbumWorkflowRunner.Object,
                _mockTrackWorkflowRunner.Object);
            
            _tracks = new List<ITrack>
            {
                new Mock<ITrack>().Object
            };

            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.FixTrackNames)
            };
        }

        [Test]
        public void Constructor_SetsDefaultStatus()
        {
            // assert
            _target.Should().NotBeNull();
            _target.Message.Should().BeEmpty();
            _target.ItemsProcessed.Should().Be(0);
            _target.ItemsTotal.Should().Be(0);
        }
        
        [Test]
        public void FixMedia_ValidatesArgs()
        {
            // assert
            Assert.Throws<ArgumentNullException>(() => _target.FixMedia(null!, _workflows));
            Assert.Throws<ArgumentNullException>(() => _target.FixMedia(_tracks, null!));
        }
        
        [Test]
        public void FixMedia_OrdersTracksByFileName()
        {
            // arrange
            var tracks = new List<ITrack>();
            const int count = 3;
            for (var i = 0; i < count; i++)
            {
                var mockTrack = new Mock<ITrack>();
                mockTrack.SetupGet(mock => mock.FileName).Returns($"t{count - i}");
                tracks.Add(mockTrack.Object);
            }

            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.MergeAlbums)
            };

            // act
            _target.FixMedia(tracks, _workflows);

            // assert
            _mockMergeAlbumsWorkflowRunner.Verify(mock => mock.Run(
                It.Is<WorkflowRunnerInfo>(info => 
                    info.Tracks[0].FileName == "t1" &&
                    info.Tracks[1].FileName == "t2" &&
                    info.Tracks[2].FileName == "t3"),
                ref It.Ref<MediaFixerStatus>.IsAny));
        }
        
        [Test]
        public void FixMedia_WhenNoTracks_ReturnsBeforeProcessing()
        {
            // arrange
            _tracks = new List<ITrack>();
            
            // act
            _target.FixMedia(_tracks, _workflows);
            
            // assert
            _target.Message.Should().BeEmpty();
            _target.ItemsProcessed.Should().Be(0);
            _target.ItemsTotal.Should().Be(0);
        }
        
        [Test]
        public void FixMedia_WhenNoWorkflows_ReturnsBeforeProcessing()
        {
            // arrange
            _workflows = new List<MediaFixerLib.Workflow.Workflow>();
            
            // act
            _target.FixMedia(_tracks, _workflows);
            
            // assert
            _target.Message.Should().BeEmpty();
            _target.ItemsProcessed.Should().Be(0);
            _target.ItemsTotal.Should().Be(0);
        }

        [Test]
        public void FixMedia_WhenHasMergeAlbumsWorkflow_RunsMergeAlbums()
        {
            // arrange
            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.MergeAlbums)
            };
            
            // act
            _target.FixMedia(_tracks, _workflows);

            // assert
            VerifyRun(_mockMergeAlbumsWorkflowRunner, Times.Once());
            VerifyRun(_mockImportTrackNamesWorkflowRunner, Times.Never());
            VerifyRun(_mockAlbumWorkflowRunner, Times.Never());
            VerifyRun(_mockTrackWorkflowRunner, Times.Never());
        }
        
        [Test]
        public void FixMedia_WhenHasInputFilePath_RunsImportTrackNames()
        {
            // arrange
            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.ImportTrackNames, fileName: "mock.txt")
            };
            
            // act
            _target.FixMedia(_tracks, _workflows);

            // assert
            VerifyRun(_mockMergeAlbumsWorkflowRunner, Times.Never());
            VerifyRun(_mockImportTrackNamesWorkflowRunner, Times.Once());
            VerifyRun(_mockAlbumWorkflowRunner, Times.Never());
            VerifyRun(_mockTrackWorkflowRunner, Times.Never());
        }
        
        [Test]
        public void FixMedia_WhenHasAlbumWorkflows_RunsUpdateAlbums()
        {
            // arrange
            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.FixCountOfTracksOnAlbum)
            };
            
            // act
            _target.FixMedia(_tracks, _workflows);

            // assert
            VerifyRun(_mockMergeAlbumsWorkflowRunner, Times.Never());
            VerifyRun(_mockImportTrackNamesWorkflowRunner, Times.Never());
            VerifyRun(_mockAlbumWorkflowRunner, Times.Once());
            VerifyRun(_mockTrackWorkflowRunner, Times.Never());
        }
        
        [Test]
        public void FixMedia_WhenHasTrackWorkflows_RunsUpdateTracks()
        {
            // arrange
            _workflows = new List<MediaFixerLib.Workflow.Workflow>
            {
                MediaFixerLib.Workflow.Workflow.Create(WorkflowName.FixTrackNames)
            };
            
            // act
            _target.FixMedia(_tracks, _workflows);

            // assert
            VerifyRun(_mockMergeAlbumsWorkflowRunner, Times.Never());
            VerifyRun(_mockImportTrackNamesWorkflowRunner, Times.Never());
            VerifyRun(_mockAlbumWorkflowRunner, Times.Never());
            VerifyRun(_mockTrackWorkflowRunner, Times.Once());
        }
        
        [Test]
        public void FixMedia_WhenAnyTracks_SavesEachTrack()
        {
            // arrange
            var mockTracks = new List<Mock<ITrack>>();
            for (var i = 0; i < 3; i++)
            {
                var mockTrack = new Mock<ITrack>();
                mockTrack.SetupGet(mock => mock.FileName).Returns($"t{i}");
                mockTracks.Add(mockTrack);
            }
            
            // act
            _target.FixMedia(mockTracks.Select(mock => mock.Object), _workflows);
            
            // assert
            foreach (var mockTrack in mockTracks)
            {
                mockTrack.Verify(mock => mock.Save(), Times.Once);
            }
        }
    }
}
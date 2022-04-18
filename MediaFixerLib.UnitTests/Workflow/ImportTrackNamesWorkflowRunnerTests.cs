using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using Moq;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class ImportTrackNamesWorkflowRunnerTests
    {
        private const string TestInputFilePath = "TestInputFile.txt";

        private ImportTrackNamesWorkflowRunner _importTrackNamesWorkflowRunner = new();
        private MediaFixerStatus _status = MediaFixerStatus.Create(0);

        private IList<ITrack> _testTracks = new List<ITrack>();

        private Mock<IWorkflowRunnerInfo> _mockWorkflowData = new();

        [SetUp]
        public void SetUp()
        {
            _importTrackNamesWorkflowRunner = new ImportTrackNamesWorkflowRunner();
            _status = MediaFixerStatus.Create(default);

            _testTracks = TestData.BuildMockAlbum().ToList();

            _mockWorkflowData = new Mock<IWorkflowRunnerInfo>();
            _mockWorkflowData.Setup(m => m.Tracks).Returns(_testTracks);
            _mockWorkflowData.Setup(m => m.InputFilePath).Returns(TestInputFilePath);

            File.WriteAllLines(TestInputFilePath, _testTracks.Select(t => t.TrackName ?? string.Empty));
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TestInputFilePath)) 
                File.Delete(TestInputFilePath);
        }

        [Test]
        public void WhenWorkflowInfoNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _importTrackNamesWorkflowRunner.Run(null!, ref _status));
        }

        [Test]
        public void WhenTracksNull_Throws()
        {
            _mockWorkflowData.Setup(m => m.Tracks).Returns((IList<ITrack>)null!);
            Assert.Throws<ArgumentException>(() => _importTrackNamesWorkflowRunner.Run(_mockWorkflowData.Object, ref _status));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void WhenInputFilePathNullOrWhiteSpace_Throws(string inputFilePath)
        {
            _mockWorkflowData.Setup(m => m.InputFilePath).Returns(inputFilePath);
            Assert.Throws<ArgumentException>(() => _importTrackNamesWorkflowRunner.Run(_mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenAnyTrackNumberZero_Throws()
        {
            // arrange
            var mockTrack = new Mock<ITrack>();
            mockTrack.SetupGet(t => t.TrackNumber).Returns(0);
            _testTracks.Add(mockTrack.Object);

            // act/assert
            Assert.Throws<MediaFixerException>(() => _importTrackNamesWorkflowRunner.Run(_mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenFileTrackCount_DoesntMatchListTrackCount_Throws()
        {
            // arrange
            File.WriteAllLines(TestInputFilePath, _testTracks.Skip(1).Select(t => t.TrackName ?? string.Empty));

            // act/assert
            Assert.Throws<MediaFixerException>(() => _importTrackNamesWorkflowRunner.Run(_mockWorkflowData.Object, ref _status));
        }

        [Test]
        public void WhenParsingFile_SkipsEmptyLines()
        {
            // arrange
            var lines = _testTracks.Select(t => t.TrackName ?? string.Empty)
                .Prepend(" ")
                .Append("  ")
                .ToArray();
            var expectedCount = lines.Length - 2;
            File.WriteAllLines(TestInputFilePath, lines);

            // act
            _importTrackNamesWorkflowRunner.Run(_mockWorkflowData.Object, ref _status);

            // assert
            Assert.AreEqual(expectedCount, _status.ItemsProcessed);
            Assert.AreEqual(expectedCount, _status.ItemsTotal);
        }

        [Test]
        public void WhenParsingFile_TrimsLines()
        {
            // arrange
            var lines = _testTracks.Select(t => $"   {t.TrackName}  ").ToArray();
            File.WriteAllLines(TestInputFilePath, lines);

            // act
            _importTrackNamesWorkflowRunner.Run(_mockWorkflowData.Object, ref _status);

            // assert
            for (var i = 0; i < lines.Length; i++)
            {
                Assert.AreEqual(lines[i].Trim(), _mockWorkflowData.Object.Tracks[i].TrackName);
            }
        }
    }
}

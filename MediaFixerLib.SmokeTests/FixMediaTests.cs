using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;
using NUnit.Framework;

namespace MediaFixerLib.SmokeTests
{
    public class Tests
    {
        private const string Path = "/Users/jasonracey/Music/iTunes/iTunes Media/Music/Grateful Dead/1966-10-07 Fillmore Auditorium, San Francisco, CA";
        private const string SearchPattern = "*.mp3";

        private readonly (int Number, string? Name)[] _expectedTrackData = 
        {
            (1, "Cream Puff War"), 
            (2, "Good Morning Little Schoolgirl"), 
            (3, "Stealin'")
        };
        
        private readonly IEnumerable<ITrack> _tracks = GetTracks(Path);

        private readonly MediaFixer _mediaFixer = new(
            new MergeAlbumsWorkflowRunner(),
            new ImportTrackNamesWorkflowRunner(),
            new AlbumWorkflowRunner(),
            new TrackWorkflowRunner());
        
        private static IEnumerable<ITrack> GetTracks(string directoryPath)
        {
            var filePaths = Directory.GetFiles(
                directoryPath.Trim(), 
                SearchPattern, 
                SearchOption.AllDirectories);

            return filePaths.Select(path => new Track(path));
        }
        
        [Test]
        public void CanFixTracks()
        {
            // arrange
            var workflows = new List<Workflow.Workflow>
            {
                Workflow.Workflow.Create(WorkflowName.FixCountOfTracksOnAlbum),
                Workflow.Workflow.Create(WorkflowName.FixGratefulDeadTracks),
                Workflow.Workflow.Create(WorkflowName.FixTrackNames),
                Workflow.Workflow.Create(WorkflowName.FixTrackNumbers)
            };
            
            // act
            _mediaFixer.FixMedia(_tracks, workflows);
            
            // assert
            foreach (var file in _tracks)
            {
                Assert.IsNotNull(file);
                Assert.IsTrue(_expectedTrackData.Contains((file.TrackNumber, file.TrackName)));
                Assert.AreEqual(_expectedTrackData.Length, file.TrackCount);
            }
        }

        [Test]
        public void CanImportTrackNames()
        {
            // arrange
            var workflows = new List<Workflow.Workflow>
            {
                Workflow.Workflow.Create(WorkflowName.ImportTrackNames, fileName: "./~/TrackNames.txt")
            };
            
            // act
            _mediaFixer.FixMedia(_tracks, workflows);
            
            // assert
            foreach (var file in _tracks)
            {
                Assert.IsNotNull(file);
                Assert.IsTrue(_expectedTrackData.Contains((file.TrackNumber, file.TrackName)));
                Assert.AreEqual(_expectedTrackData.Length, file.TrackCount);
            }
        }
        
        [Test]
        public void CanFindAndReplace()
        {
            // arrange
            var workflows = new List<Workflow.Workflow>
            {
                Workflow.Workflow.Create(WorkflowName.FindAndReplace, oldValue: "XXX", newValue: string.Empty)
            };
            
            // act
            _mediaFixer.FixMedia(_tracks, workflows);
            
            // assert
            foreach (var file in _tracks)
            {
                Assert.IsNotNull(file);
                Assert.IsTrue(_expectedTrackData.Contains((file.TrackNumber, file.TrackName)));
                Assert.AreEqual(_expectedTrackData.Length, file.TrackCount);
            }
        }
        
        [Test]
        public void CanMergeAlbums()
        {
            // arrange
            var workflows = new List<Workflow.Workflow>
            {
                Workflow.Workflow.Create(WorkflowName.MergeAlbums)
            };
            
            // act
            _mediaFixer.FixMedia(_tracks, workflows);
            
            // assert
            foreach (var file in _tracks)
            {
                Assert.IsNotNull(file);
                Assert.IsTrue(_expectedTrackData.Contains((file.TrackNumber, file.TrackName)));
                Assert.AreEqual(_expectedTrackData.Length, file.TrackCount);
            }
        }

        [Test]
        public void CanSetAlbumNames()
        {
            // arrange
            const string path = "/Users/jasonracey/Music/iTunes/iTunes Media/Music/Juan Atkins/2020-09-25 Movement Selects Vol.1";
            (int Number, string? Name)[] expectedTrackData = 
            {
                (1, "2020-09-25 Movement Selects Vol.1")
            };
            var files = GetTracks(path).ToArray();
            var workflows = new List<Workflow.Workflow>
            {
                Workflow.Workflow.Create(WorkflowName.SetAlbumNames)
            };
            
            // act
            _mediaFixer.FixMedia(files, workflows);
            
            // assert
            foreach (var file in files)
            {
                Assert.IsNotNull(file);
                Assert.IsTrue(expectedTrackData.Contains((file.TrackNumber, file.TrackName)));
                Assert.AreEqual(expectedTrackData.Length, file.TrackCount);
            }
        }
    }
}
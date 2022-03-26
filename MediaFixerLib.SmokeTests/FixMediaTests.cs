using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaFixerLib.Workflow;
using NUnit.Framework;

namespace MediaFixerLib.SmokeTests
{
    public class Tests
    {
        private const string Path = "/Users/jasonracey/Music/iTunes/iTunes Media/Music/Grateful Dead/1966-10-07 Fillmore Auditorium, San Francisco, CA";
        private const string SearchPattern = "*.mp3";

        private readonly (uint Number, string Name)[] _tracks = 
        {
            (1, "Cream Puff War"), 
            (2, "Good Morning Little Schoolgirl"), 
            (3, "Stealin'")
        };
        
        private readonly string[] _filePaths = Directory.GetFiles(Path, SearchPattern, SearchOption.AllDirectories);

        private readonly MediaFixer _mediaFixer = new(
            new MergeAlbumsWorkflowRunner(),
            new ImportTrackNamesWorkflowRunner(),
            new AlbumWorkflowRunner(),
            new TrackWorkflowRunner());
        
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
            _mediaFixer.FixMedia(_filePaths, workflows);
            
            // assert
            foreach (var filePath in _filePaths)
            {
                var file = TagLib.File.Create(filePath);
                Assert.IsNotNull(file);
                Assert.IsNotNull(file.Tag);
                Assert.IsTrue(_tracks.Contains((file.Tag.Track, file.Tag.Title)));
                Assert.AreEqual(_tracks.Length, file.Tag.TrackCount);
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
            _mediaFixer.FixMedia(_filePaths, workflows);
            
            // assert
            foreach (var filePath in _filePaths)
            {
                var file = TagLib.File.Create(filePath);
                Assert.IsNotNull(file);
                Assert.IsNotNull(file.Tag);
                Assert.IsTrue(_tracks.Contains((file.Tag.Track, file.Tag.Title)));
                Assert.AreEqual(_tracks.Length, file.Tag.TrackCount);
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
            _mediaFixer.FixMedia(_filePaths, workflows);
            
            // assert
            foreach (var filePath in _filePaths)
            {
                var file = TagLib.File.Create(filePath);
                Assert.IsNotNull(file);
                Assert.IsNotNull(file.Tag);
                Assert.IsTrue(_tracks.Contains((file.Tag.Track, file.Tag.Title)));
                Assert.AreEqual(_tracks.Length, file.Tag.TrackCount);
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
            _mediaFixer.FixMedia(_filePaths, workflows);
            
            // assert
            foreach (var filePath in _filePaths)
            {
                var file = TagLib.File.Create(filePath);
                Assert.IsNotNull(file);
                Assert.IsNotNull(file.Tag);
                Assert.IsTrue(_tracks.Contains((file.Tag.Track, file.Tag.Title)));
                Assert.AreEqual(_tracks.Length, file.Tag.TrackCount);
            }
        }

        [Test]
        public void CanSetAlbumNames()
        {
            // arrange
            const string path = "/Users/jasonracey/Music/iTunes/iTunes Media/Music/Juan Atkins/2020-09-25 Movement Selects Vol.1";
            const string searchPattern = "*.mp3";
            (uint Number, string Name)[] tracks = 
            {
                (1, "2020-09-25 Movement Selects Vol.1")
            };
            var filePaths = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            var workflows = new List<Workflow.Workflow>
            {
                Workflow.Workflow.Create(WorkflowName.SetAlbumNames)
            };
            
            // act
            _mediaFixer.FixMedia(filePaths, workflows);
            
            // assert
            foreach (var filePath in filePaths)
            {
                var file = TagLib.File.Create(filePath);
                Assert.IsNotNull(file);
                Assert.IsNotNull(file.Tag);
                Assert.IsTrue(tracks.Contains((file.Tag.Track, file.Tag.Title)));
                Assert.AreEqual(tracks.Length, file.Tag.TrackCount);
            }
        }
    }
}
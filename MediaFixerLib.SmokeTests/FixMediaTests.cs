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
        
        private readonly IEnumerable<TagLib.File> _files = GetFiles(Path);

        private readonly MediaFixer _mediaFixer = new(
            new MergeAlbumsWorkflowRunner(),
            new ImportTrackNamesWorkflowRunner(),
            new AlbumWorkflowRunner(),
            new TrackWorkflowRunner());
        
        private static IEnumerable<TagLib.File> GetFiles(string directoryPath)
        {
            var filePaths = Directory.GetFiles(
                directoryPath.Trim(), 
                SearchPattern, 
                SearchOption.AllDirectories);

            return filePaths.Select(TagLib.File.Create);
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
            _mediaFixer.FixMedia(_files, workflows);
            
            // assert
            foreach (var file in _files)
            {
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
            _mediaFixer.FixMedia(_files, workflows);
            
            // assert
            foreach (var file in _files)
            {
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
            _mediaFixer.FixMedia(_files, workflows);
            
            // assert
            foreach (var file in _files)
            {
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
            _mediaFixer.FixMedia(_files, workflows);
            
            // assert
            foreach (var file in _files)
            {
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
            (uint Number, string Name)[] tracks = 
            {
                (1, "2020-09-25 Movement Selects Vol.1")
            };
            var files = GetFiles(path).ToArray();
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
                Assert.IsNotNull(file.Tag);
                Assert.IsTrue(tracks.Contains((file.Tag.Track, file.Tag.Title)));
                Assert.AreEqual(tracks.Length, file.Tag.TrackCount);
            }
        }
    }
}
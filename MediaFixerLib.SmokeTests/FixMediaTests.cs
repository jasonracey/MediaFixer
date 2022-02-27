using System.Collections.Generic;
using System.IO;
using MediaFixerLib.Workflow;
using NUnit.Framework;

namespace MediaFixerLib.SmokeTests;

public class Tests
{
    private readonly MediaFixer _mediaFixer = new(
        new MergeAlbumsWorkflowRunner(),
        new ImportTrackNamesWorkflowRunner(),
        new AlbumWorkflowRunner(),
        new TrackWorkflowRunner());
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CanFixTrackNames()
    {
        // arrange
        const string path = "/Users/jasonracey/Music/iTunes/iTunes Media/Music/Svarga/Under the Black Spell";
        const string searchPattern = "*.*";
        var filePaths = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
        var workflows = new List<Workflow.Workflow>
        {
            Workflow.Workflow.Create(WorkflowName.FixTrackNames)
        };
        
        // act
        _mediaFixer.FixMedia(filePaths, workflows);
        
        // assert
        Assert.Pass();
    }
}
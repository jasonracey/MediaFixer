using System;
using FluentAssertions;
using MediaFixerLib.Workflow;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class WorkflowTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void NameIsRequired(string name)
        {
            Assert.Throws<ArgumentNullException>(() => MediaFixerLib.Workflow.Workflow.Create(name: name));
        }

        [Test]
        public void MergeAlbums()
        {
            // arrange
            const string name = WorkflowName.MergeAlbums;

            // act
            var workflow = MediaFixerLib.Workflow.Workflow.Create(name);

            // assert
            workflow.Should().NotBeNull();
            workflow.Name.Should().Be(name);
            workflow.FileName.Should().BeNull();
            workflow.OldValue.Should().BeNull();
            workflow.NewValue.Should().BeNull();
            workflow.Type.Should().Be(WorkflowType.None);
        }

        [Test]
        public void ImportTrackNames()
        {
            // arrange
            const string name = WorkflowName.ImportTrackNames;
            var fileName = Guid.NewGuid().ToString();

            // act
            var workflow = MediaFixerLib.Workflow.Workflow.Create(name, fileName: fileName);

            // assert
            workflow.Should().NotBeNull();
            workflow.Name.Should().Be(name);
            workflow.FileName.Should().Be(fileName);
            workflow.OldValue.Should().BeNull();
            workflow.NewValue.Should().BeNull();
            workflow.Type.Should().Be(WorkflowType.None);
        }

        [Test]
        public void FindAndReplace()
        {
            // arrange
            const string name = WorkflowName.FindAndReplace;
            var oldValue = Guid.NewGuid().ToString();
            var newValue = Guid.NewGuid().ToString();

            // act
            var workflow = MediaFixerLib.Workflow.Workflow.Create(name, oldValue: oldValue, newValue: newValue);

            // assert
            workflow.Should().NotBeNull();
            workflow.Name.Should().Be(name);
            workflow.FileName.Should().BeNull();
            workflow.OldValue.Should().Be(oldValue);
            workflow.NewValue.Should().Be(newValue);
            workflow.Type.Should().Be(WorkflowType.Track);
        }

        [TestCase(WorkflowName.FixCountOfTracksOnAlbum)]
        [TestCase(WorkflowName.FixTrackNumbers)]
        public void AlbumWorkflows(string name)
        {
            // act
            var workflow = MediaFixerLib.Workflow.Workflow.Create(name);

            // assert
            workflow.Should().NotBeNull();
            workflow.Name.Should().Be(name);
            workflow.FileName.Should().BeNull();
            workflow.OldValue.Should().BeNull();
            workflow.NewValue.Should().BeNull();
            workflow.Type.Should().Be(WorkflowType.Album);
        }

        [TestCase(WorkflowName.FixGratefulDeadTracks)]
        [TestCase(WorkflowName.FixTrackNames)]
        [TestCase(WorkflowName.SetAlbumNames)]
        public void TrackWorkflows(string name)
        {
            // act
            var workflow = MediaFixerLib.Workflow.Workflow.Create(name);

            // assert
            workflow.Should().NotBeNull();
            workflow.Name.Should().Be(name);
            workflow.FileName.Should().BeNull();
            workflow.OldValue.Should().BeNull();
            workflow.NewValue.Should().BeNull();
            workflow.Type.Should().Be(WorkflowType.Track);
        }
    }
}

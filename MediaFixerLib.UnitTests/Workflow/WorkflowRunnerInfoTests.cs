using System;
using MediaFixerLib.Workflow;
using NUnit.Framework;

namespace MediaFixerLib.UnitTests.Workflow
{
    [TestFixture]
    public class WorkflowRunnerInfoTests
    {
        [Test]
        public void Constructor_TracksMustNotBeNull()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new WorkflowRunnerInfo(tracks: null!));
        }
    }
}

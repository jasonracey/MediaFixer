using System;
using System.Collections.Generic;
using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public class WorkflowRunnerInfo
    {
        public WorkflowRunnerInfo(IList<ITrack> tracks, IEnumerable<Workflow>? workflows = null, string? inputFilePath = null)
        {
            Tracks = tracks ?? throw new ArgumentNullException(nameof(tracks));
            Workflows = workflows;
            InputFilePath = inputFilePath;
        }

        public IList<ITrack> Tracks { get; }

        public IEnumerable<Workflow>? Workflows { get; }

        public string? InputFilePath { get; }
    }
}
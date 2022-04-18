using System;
using System.Collections.Generic;
using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public interface IWorkflowRunnerInfo
    {
        IList<ITrack> Tracks { get; }
        IEnumerable<Workflow>? Workflows { get; }
        string? InputFilePath { get; }
    }

    public class WorkflowRunnerInfo : IWorkflowRunnerInfo
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
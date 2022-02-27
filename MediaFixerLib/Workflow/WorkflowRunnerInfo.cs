namespace MediaFixerLib.Workflow;

public class WorkflowRunnerInfo
{
    public WorkflowRunnerInfo(IList<TagLib.File> tracks, IEnumerable<Workflow>? workflows = null, string? inputFilePath = null)
    {
        Tracks = tracks ?? throw new ArgumentNullException(nameof(tracks));
        Workflows = workflows;
        InputFilePath = inputFilePath;
    }

    public IList<TagLib.File> Tracks { get; }

    public IEnumerable<Workflow>? Workflows { get; }

    public string? InputFilePath { get; }
}
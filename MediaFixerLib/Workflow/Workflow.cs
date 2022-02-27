namespace MediaFixerLib.Workflow;

public class Workflow
{
    public string Name { get; }

    public string? FileName { get; }

    public string? NewValue { get; }

    public string? OldValue { get; }

    public WorkflowType? Type { get; }

    private Workflow(
        string name,
        string? fileName = null,
        string? oldValue = null,
        string? newValue = null,
        WorkflowType? type = WorkflowType.None)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }
            
        Name = name;
        FileName = fileName;
        OldValue = oldValue;
        NewValue = newValue;
        Type = type;
    }

    public static Workflow Create(
        string name, 
        string? oldValue = null, 
        string? newValue = null, 
        string? fileName = null)
    {
        return name switch
        {
            WorkflowName.MergeAlbums => new Workflow(name),
            WorkflowName.ImportTrackNames => new Workflow(name, fileName),
            WorkflowName.FixCountOfTracksOnAlbum => new Workflow(name, type: WorkflowType.Album),
            WorkflowName.FixTrackNumbers => new Workflow(name, type: WorkflowType.Album),
            WorkflowName.FindAndReplace => new Workflow(name, oldValue: oldValue, newValue: newValue, type: WorkflowType.Track),
            _ => new Workflow(name, type: WorkflowType.Track)
        };
    }
}
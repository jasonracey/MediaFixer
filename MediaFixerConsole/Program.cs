using CommandLine;
using MediaFixerConsole;
using MediaFixerLib;
using MediaFixerLib.Workflow;

Console.WriteLine();
Console.WriteLine("Media Fixer");
Console.WriteLine("by https://github.com/jasonracey");
Console.WriteLine();

var mediaFixer = new MediaFixer(
    new MergeAlbumsWorkflowRunner(),
    new ImportTrackNamesWorkflowRunner(),
    new AlbumWorkflowRunner(),
    new TrackWorkflowRunner());

Parser.Default
    .ParseArguments<Options>(args)
    .WithParsed(RunSelectedOptions);

Console.WriteLine("Done!");
Console.WriteLine();
Console.WriteLine("Thanks for using Media Fixer.");
Console.WriteLine();

void RunSelectedOptions(Options options)
{
    if (string.IsNullOrWhiteSpace(options.Directory))
    {
        Console.WriteLine("Please specify a directory.");
    }
    else if (!Directory.Exists(options.Directory))
    {
        Console.WriteLine("Directory not found.");
    }
    else if (!TryGetFiles(options.Directory, out var filePaths))
    {
        Console.WriteLine("Directory contains no files.");
    }
    else
    {
        var workflows = new HashSet<Workflow>();

        if (options.FindAndReplace.HasValue)
            workflows.Add(Workflow.Create(WorkflowName.FindAndReplace, options.FindAndReplace.Value.OldValue, options.FindAndReplace.Value.NewValue));
        if (options.FixCountOfTracksOnAlbum)
            workflows.Add(Workflow.Create(name: WorkflowName.FixCountOfTracksOnAlbum));
        if (options.FixGratefulDeadTracks)
            workflows.Add(Workflow.Create(name: WorkflowName.FixGratefulDeadTracks));
        if (options.FixTrackNames)
            workflows.Add(Workflow.Create(name: WorkflowName.FixTrackNames));
        if (options.FixTrackNumbers)
            workflows.Add(Workflow.Create(name: WorkflowName.FixTrackNumbers));
        if (!string.IsNullOrWhiteSpace(options.ImportTrackNames))
            workflows.Add(Workflow.Create(name: WorkflowName.ImportTrackNames, fileName: options.ImportTrackNames));
        if (options.MergeAlbums)
            workflows.Add(Workflow.Create(name: WorkflowName.MergeAlbums));
        if (options.SetAlbumNames)
            workflows.Add(Workflow.Create(name: WorkflowName.SetAlbumNames));

        mediaFixer.FixMedia(filePaths, workflows);
    }
}

bool TryGetFiles(string? path, out string[] filePaths)
{
    if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentNullException(nameof(path));
    
    filePaths = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
    return filePaths.Any();
}
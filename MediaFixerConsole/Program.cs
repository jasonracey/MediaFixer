using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using MediaFixerLib;
using MediaFixerLib.Workflow;

namespace MediaFixerConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
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
                else if (!TryGetFiles(options.Directory, out var files))
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

                    mediaFixer.FixMedia(files, workflows);
                }
            }
            
            bool TryGetFiles(string? directoryPath, out IEnumerable<TagLib.File> files)
            {
                if (string.IsNullOrWhiteSpace(directoryPath))
                    throw new ArgumentNullException(nameof(directoryPath));
                
                var filePaths = Directory.GetFiles(
                    directoryPath.Trim(), 
                    ".mp3", 
                    SearchOption.AllDirectories);

                files = filePaths.Select(TagLib.File.Create);

                return files.Any();
            }
        }
    }
}
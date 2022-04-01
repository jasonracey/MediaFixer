using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Workflow;

namespace MediaFixerLib
{
    public class MediaFixer
    {
        private readonly IWorkflowRunner _mergeAlbumsWorkflowRunner;
        private readonly IWorkflowRunner _importTrackNamesWorkflowRunner;
        private readonly IWorkflowRunner _albumWorkflowRunner;
        private readonly IWorkflowRunner _trackWorkflowRunner;

        private MediaFixerStatus _mediaFixerStatus;

        public MediaFixer(
            IWorkflowRunner mergeAlbumsWorkflowRunner,
            IWorkflowRunner importTrackNamesWorkflowRunner,
            IWorkflowRunner albumWorkflowRunner,
            IWorkflowRunner trackWorkflowRunner)
        {
            _mergeAlbumsWorkflowRunner = mergeAlbumsWorkflowRunner ?? throw new ArgumentNullException(nameof(mergeAlbumsWorkflowRunner));
            _importTrackNamesWorkflowRunner = importTrackNamesWorkflowRunner ?? throw new ArgumentNullException(nameof(importTrackNamesWorkflowRunner));
            _albumWorkflowRunner = albumWorkflowRunner ?? throw new ArgumentNullException(nameof(albumWorkflowRunner));
            _trackWorkflowRunner = trackWorkflowRunner ?? throw new ArgumentNullException(nameof(trackWorkflowRunner));

            _mediaFixerStatus = MediaFixerStatus.Create(default);
        }

        public string Message => _mediaFixerStatus.Message;

        public int ItemsProcessed => _mediaFixerStatus.ItemsProcessed;

        public int ItemsTotal => _mediaFixerStatus.ItemsTotal;

        public void FixMedia(
            IEnumerable<string> filePaths,
            IEnumerable<Workflow.Workflow> workflows)
        {
            if (filePaths == null)
                throw new ArgumentNullException(nameof(filePaths));
            if (workflows == null)
                throw new ArgumentNullException(nameof(workflows));
            
            var orderedFilePathsArray = filePaths
                .OrderBy(path => path)
                .ToArray();
            if (orderedFilePathsArray.Length == 0)
            {
                return;
            }
            
            var workflowArray = workflows.ToArray();
            if (workflowArray.Length == 0)
            {
                return;
            }

            _mediaFixerStatus = MediaFixerStatus.Create(orderedFilePathsArray.Length, MediaFixerStatus.LoadingSelectedFiles);

            var filesToFix = new List<TagLib.File>();

            foreach (var filePath in orderedFilePathsArray)
            {
                filesToFix.Add(TagLib.File.Create(filePath));
                _mediaFixerStatus.ItemProcessed();
            }

            if (workflowArray.Any(workflow => workflow.Name == WorkflowName.MergeAlbums))
            {
                _mergeAlbumsWorkflowRunner.Run(new WorkflowRunnerInfo(filesToFix), ref _mediaFixerStatus);
            }

            var inputFilePath = workflowArray
                .FirstOrDefault(workflow => workflow.Name == WorkflowName.ImportTrackNames)?
                .FileName;

            if (!string.IsNullOrWhiteSpace(inputFilePath))
            {
                _importTrackNamesWorkflowRunner.Run(new WorkflowRunnerInfo(filesToFix, inputFilePath: inputFilePath), ref _mediaFixerStatus);
            }

            var albumWorkflows = workflowArray
                .Where(workflow => workflow.Type == WorkflowType.Album)
                .ToArray();

            if (albumWorkflows.Any())
            {
                _albumWorkflowRunner.Run(new WorkflowRunnerInfo(filesToFix, albumWorkflows), ref _mediaFixerStatus);
            }

            var trackWorkflows = workflowArray
                .Where(workflow => workflow.Type == WorkflowType.Track)
                .ToArray();

            if (trackWorkflows.Any())
            {
                _trackWorkflowRunner.Run(new WorkflowRunnerInfo(filesToFix, trackWorkflows), ref _mediaFixerStatus);
            }

            foreach (var file in filesToFix)
            {
                file.Save();
            }
        }
    }
}
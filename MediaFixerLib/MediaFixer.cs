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
            IEnumerable<TagLib.File> files,
            IEnumerable<Workflow.Workflow> workflows)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files));
            if (workflows == null)
                throw new ArgumentNullException(nameof(workflows));
            
            var orderedFileArray = files
                .OrderBy(file => file.Name)
                .ToArray();
            if (orderedFileArray.Length == 0)
            {
                return;
            }
            
            var workflowArray = workflows.ToArray();
            if (workflowArray.Length == 0)
            {
                return;
            }

            _mediaFixerStatus = MediaFixerStatus.Create(orderedFileArray.Length, MediaFixerStatus.LoadingSelectedFiles);

            if (workflowArray.Any(workflow => workflow.Name == WorkflowName.MergeAlbums))
            {
                _mergeAlbumsWorkflowRunner.Run(new WorkflowRunnerInfo(orderedFileArray), ref _mediaFixerStatus);
            }

            var inputFilePath = workflowArray
                .FirstOrDefault(workflow => workflow.Name == WorkflowName.ImportTrackNames)?
                .FileName;

            if (!string.IsNullOrWhiteSpace(inputFilePath))
            {
                _importTrackNamesWorkflowRunner.Run(new WorkflowRunnerInfo(orderedFileArray, inputFilePath: inputFilePath), ref _mediaFixerStatus);
            }

            var albumWorkflows = workflowArray
                .Where(workflow => workflow.Type == WorkflowType.Album)
                .ToArray();

            if (albumWorkflows.Any())
            {
                _albumWorkflowRunner.Run(new WorkflowRunnerInfo(orderedFileArray, albumWorkflows), ref _mediaFixerStatus);
            }

            var trackWorkflows = workflowArray
                .Where(workflow => workflow.Type == WorkflowType.Track)
                .ToArray();

            if (trackWorkflows.Any())
            {
                _trackWorkflowRunner.Run(new WorkflowRunnerInfo(orderedFileArray, trackWorkflows), ref _mediaFixerStatus);
            }

            foreach (var file in orderedFileArray)
            {
                file.Save();
            }
        }
    }
}
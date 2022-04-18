using System;
using System.Linq;
using MediaFixerLib.Fixer;

namespace MediaFixerLib.Workflow
{
    public class AlbumWorkflowRunner : IWorkflowRunner
    {
        public void Run(IWorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus)
        {
            if (workflowRunnerInfo == null) throw new ArgumentNullException(nameof(workflowRunnerInfo));
            if (workflowRunnerInfo.Tracks == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Tracks)} must not be null");
            if (workflowRunnerInfo.Workflows == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Workflows)} must not be null");

            var albums = AlbumBuilder.BuildAlbums(workflowRunnerInfo.Tracks, ref mediaFixerStatus);

            mediaFixerStatus = MediaFixerStatus.Create(albums.Count, MediaFixerStatus.RunningAlbumWorkflows);

            foreach (var tracks in albums.Select(album => album.Value))
            {
                // iTunes deletes TrackCount if TrackNumber is set to a higher value, so set number before count
                // in case any old track numbers are higher than new track count.
                if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FixTrackNumbers))
                {
                    TrackNumberFixer.FixTrackNumbers(tracks);
                }

                if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FixCountOfTracksOnAlbum))
                {
                    TrackCountFixer.FixTrackCounts(tracks);
                }

                mediaFixerStatus.ItemProcessed();
            }
        }
    }
}
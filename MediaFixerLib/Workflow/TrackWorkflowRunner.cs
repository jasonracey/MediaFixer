using MediaFixerLib.Fixer;

namespace MediaFixerLib.Workflow;

public class TrackWorkflowRunner : IWorkflowRunner
{
    public void Run(WorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus)
    {
        if (workflowRunnerInfo == null) throw new ArgumentNullException(nameof(workflowRunnerInfo));
        if (workflowRunnerInfo.Tracks == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Tracks)} must not be null");
        if (workflowRunnerInfo.Workflows == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Workflows)} must not be null");

        mediaFixerStatus = MediaFixerStatus.Create(workflowRunnerInfo.Tracks.Count, MediaFixerStatus.RunningTrackWorkflows);

        foreach (var track in workflowRunnerInfo.Tracks)
        {
            if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.SetAlbumNames))
            {
                if (string.IsNullOrWhiteSpace(track.Tag.Album))
                {
                    track.Tag.Album = track.Tag.Title;
                }
            }

            if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FindAndReplace))
            {
                var findAndReplace = workflowRunnerInfo.Workflows.First(item => item.Name == WorkflowName.FindAndReplace);
                if (!string.IsNullOrEmpty(findAndReplace.OldValue))
                {
                    track.Tag.Title = track.Tag.Title.Replace(findAndReplace.OldValue, findAndReplace.NewValue);
                }
            }

            if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FixTrackNames))
            {
                track.Tag.Title = TrackNameFixer.FixTrackName(track.Tag.Title);
            }

            if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FixGratefulDeadTracks))
            {
                track.Tag.Title = GratefulDeadTrackNameFixer.FixTrackName(track.Tag.Title);
                if (string.IsNullOrWhiteSpace(track.Tag.Comment))
                {
                    throw new MediaFixerException(MediaFixerException.Reason.MissingTrackComment);
                }

                track.Tag.Comment = track.Tag.Comment.Replace("https://archive.org/details/", string.Empty);
            }

            mediaFixerStatus.ItemProcessed();
        }
    }
}
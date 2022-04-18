using System;
using System.Linq;
using MediaFixerLib.Fixer;

namespace MediaFixerLib.Workflow
{
    public class TrackWorkflowRunner : IWorkflowRunner
    {
        public void Run(IWorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus)
        {
            if (workflowRunnerInfo == null) throw new ArgumentNullException(nameof(workflowRunnerInfo));
            if (workflowRunnerInfo.Tracks == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Tracks)} must not be null");
            if (workflowRunnerInfo.Workflows == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Workflows)} must not be null");

            mediaFixerStatus = MediaFixerStatus.Create(workflowRunnerInfo.Tracks.Count, MediaFixerStatus.RunningTrackWorkflows);

            foreach (var track in workflowRunnerInfo.Tracks)
            {
                if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.SetAlbumNames))
                {
                    if (string.IsNullOrWhiteSpace(track.AlbumName))
                    {
                        track.AlbumName = track.TrackName;
                    }
                }

                if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FindAndReplace))
                {
                    var findAndReplace = workflowRunnerInfo.Workflows.First(item => item.Name == WorkflowName.FindAndReplace);
                    if (!string.IsNullOrEmpty(findAndReplace.OldValue) && !string.IsNullOrWhiteSpace(track.TrackName))
                    {
                        track.TrackName = track.TrackName.Replace(findAndReplace.OldValue, findAndReplace.NewValue);
                    }
                }

                if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FixTrackNames))
                {
                    if (string.IsNullOrWhiteSpace(track.TrackName))
                    {
                        var title = track.FileName
                            .Split("/")?
                            .Last()?
                            .Replace(".mp3", string.Empty);
                        
                        track.TrackName = title ?? string.Empty;
                    }
                    
                    track.TrackName = TrackNameFixer.FixTrackName(track.TrackName);
                }

                if (workflowRunnerInfo.Workflows.Any(workflow => workflow.Name == WorkflowName.FixGratefulDeadTracks))
                {
                    if (!string.IsNullOrWhiteSpace(track.TrackName))
                    {
                        track.TrackName = GratefulDeadTrackNameFixer.FixTrackName(track.TrackName);
                    }
                    
                    track.Comment = track.Comment?.Replace("https://archive.org/details/", string.Empty);
                }

                mediaFixerStatus.ItemProcessed();
            }
        }
    }
}
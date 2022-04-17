using System;
using System.Collections.Generic;
using System.IO;

namespace MediaFixerLib.Workflow
{
    public class ImportTrackNamesWorkflowRunner : IWorkflowRunner
    {
        public void Run(WorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus)
        {
            if (workflowRunnerInfo == null) throw new ArgumentNullException(nameof(workflowRunnerInfo));
            if (workflowRunnerInfo.Tracks == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Tracks)} must not be null");
            if (string.IsNullOrWhiteSpace(workflowRunnerInfo.InputFilePath)) throw new ArgumentException($"{nameof(workflowRunnerInfo.InputFilePath)} must not be null");

            mediaFixerStatus = MediaFixerStatus.Create(workflowRunnerInfo.Tracks.Count, MediaFixerStatus.ReadingTrackNames);

            var newTrackNames = new List<string>();

            foreach (var line in File.ReadAllLines(workflowRunnerInfo.InputFilePath))
            {
                var trimmedLine = line.Trim();
                if (trimmedLine != string.Empty)
                {
                    newTrackNames.Add(trimmedLine);
                    mediaFixerStatus.ItemProcessed();
                }
            }

            if (workflowRunnerInfo.Tracks.Count != newTrackNames.Count)
            {
                throw new MediaFixerException(MediaFixerException.Reason.ImportCountMismatch);
            }

            mediaFixerStatus = MediaFixerStatus.Create(workflowRunnerInfo.Tracks.Count, MediaFixerStatus.AssigningTrackNames);

            for (var i = 0; i < workflowRunnerInfo.Tracks.Count; i++)
            {
                workflowRunnerInfo.Tracks[i].TrackName = newTrackNames[i];
                mediaFixerStatus.ItemProcessed();
            }
        }
    }
}
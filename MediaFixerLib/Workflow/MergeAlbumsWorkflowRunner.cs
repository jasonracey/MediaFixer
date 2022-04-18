using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Fixer;

namespace MediaFixerLib.Workflow
{
    public class MergeAlbumsWorkflowRunner : IWorkflowRunner
    {
        public void Run(IWorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus)
        {
            if (workflowRunnerInfo == null) throw new ArgumentNullException(nameof(workflowRunnerInfo));
            if (workflowRunnerInfo.Tracks == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Tracks)} must not be null");

            mediaFixerStatus = MediaFixerStatus.Create(workflowRunnerInfo.Tracks.Count, MediaFixerStatus.GeneratingAlbumGroups);

            var albumGroups = new Dictionary<string, IDictionary<string, IList<ITrack>>>();

            foreach (var track in workflowRunnerInfo.Tracks)
            {
                if (string.IsNullOrWhiteSpace(track.AlbumName))
                {
                    throw new MediaFixerException(MediaFixerException.Reason.MissingAlbumName);
                }
                
                var newAlbumName = track.AlbumName.FixAlbumName();

                if (!albumGroups.ContainsKey(newAlbumName))
                {
                    albumGroups.Add(newAlbumName, new SortedDictionary<string, IList<ITrack>>());
                    albumGroups[newAlbumName].Add(track.AlbumName, new List<ITrack> { track });
                }
                else if (!albumGroups[newAlbumName].ContainsKey(track.AlbumName))
                {
                    albumGroups[newAlbumName].Add(track.AlbumName, new List<ITrack> { track });
                }
                else
                {
                    albumGroups[newAlbumName][track.AlbumName].Add(track);
                }

                mediaFixerStatus.ItemProcessed();
            }

            foreach (var album in albumGroups.SelectMany(albumGroup => albumGroup.Value.Values))
            {
                var comparer = TrackComparerFactory.GetTrackComparer(album);
                album.ToList().Sort(comparer);
            }

            mediaFixerStatus = MediaFixerStatus.Create(albumGroups.Count, MediaFixerStatus.RunningMergeAlbums);

            foreach (var albumGroup in albumGroups)
            {
                var newAlbumName = albumGroup.Key;
                var trackCount = albumGroup.Value.Sum(album => album.Value.Count);
                var trackNumber = 1;

                foreach (var track in albumGroup.Value.SelectMany(album => album.Value))
                {
                    track.AlbumName = newAlbumName;
                    track.DiscCount = 1;
                    track.DiscNumber = 1;
                    track.TrackNumber = trackNumber++;
                    track.TrackCount = trackCount;
                }

                mediaFixerStatus.ItemProcessed();
            }
        }
    }
}
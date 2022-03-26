using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Fixer;

namespace MediaFixerLib.Workflow
{
    public class MergeAlbumsWorkflowRunner : IWorkflowRunner
{
    public void Run(WorkflowRunnerInfo workflowRunnerInfo, ref MediaFixerStatus mediaFixerStatus)
    {
        if (workflowRunnerInfo == null) throw new ArgumentNullException(nameof(workflowRunnerInfo));
        if (workflowRunnerInfo.Tracks == null) throw new ArgumentException($"{nameof(workflowRunnerInfo.Tracks)} must not be null");

        mediaFixerStatus = MediaFixerStatus.Create(workflowRunnerInfo.Tracks.Count, MediaFixerStatus.GeneratingAlbumGroups);

        var albumGroups = new Dictionary<string, IDictionary<string, IList<TagLib.File>>>();

        foreach (var track in workflowRunnerInfo.Tracks)
        {
            var newAlbumName = track.Tag.Album.FixAlbumName();

            if (!albumGroups.ContainsKey(newAlbumName))
            {
                albumGroups.Add(newAlbumName, new SortedDictionary<string, IList<TagLib.File>>());
                albumGroups[newAlbumName].Add(track.Tag.Album, new List<TagLib.File> { track });
            }
            else if (!albumGroups[newAlbumName].ContainsKey(track.Tag.Album))
            {
                albumGroups[newAlbumName].Add(track.Tag.Album, new List<TagLib.File> { track });
            }
            else
            {
                albumGroups[newAlbumName][track.Tag.Album].Add(track);
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
                track.Tag.Album = newAlbumName;
                track.Tag.DiscCount = 1;
                track.Tag.Disc = 1;

                // iTunes doesn't allow TrackCount to be set to a value higher than TrackNumber, so set number 
                // before count in case any old track numbers are higher than new track count.
                track.Tag.Track = (uint)trackNumber++;
                track.Tag.TrackCount = (uint)trackCount;
            }

            mediaFixerStatus.ItemProcessed();
        }
    }
}
}
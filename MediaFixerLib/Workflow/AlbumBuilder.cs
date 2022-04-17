using System;
using System.Collections.Generic;
using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public static class AlbumBuilder
    {
        public static IDictionary<string, IList<ITrack>> BuildAlbums(IList<ITrack> tracksToFix, ref MediaFixerStatus mediaFixerStatus)
        {
            if (tracksToFix == null) throw new ArgumentNullException(nameof(tracksToFix));

            mediaFixerStatus = MediaFixerStatus.Create(tracksToFix.Count, MediaFixerStatus.GeneratingAlbumList);

            var albums = new SortedDictionary<string, IList<ITrack>>();

            foreach (var track in tracksToFix)
            {
                if (string.IsNullOrWhiteSpace(track.AlbumName))
                {
                    var directory = track.TrackName?
                        .Split("/")?[^2];

                    track.AlbumName = directory;
                }

                if (string.IsNullOrWhiteSpace(track.AlbumName))
                {
                    throw new MediaFixerException(MediaFixerException.Reason.MissingAlbumName);
                }

                if (!albums.ContainsKey(track.AlbumName))
                {
                    albums.Add(track.AlbumName, new List<ITrack> { track });
                }
                else
                {
                    albums[track.AlbumName].Add(track);
                }

                mediaFixerStatus.ItemProcessed();
            }

            return albums;
        }
    }
}
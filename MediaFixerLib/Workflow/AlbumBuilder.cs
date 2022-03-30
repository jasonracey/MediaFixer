using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaFixerLib.Workflow
{
    public static class AlbumBuilder
    {
        public static IDictionary<string, IList<TagLib.File>> BuildAlbums(IList<TagLib.File> tracksToFix, ref MediaFixerStatus mediaFixerStatus)
        {
            if (tracksToFix == null) throw new ArgumentNullException(nameof(tracksToFix));

            mediaFixerStatus = MediaFixerStatus.Create(tracksToFix.Count, MediaFixerStatus.GeneratingAlbumList);

            var albums = new SortedDictionary<string, IList<TagLib.File>>();

            foreach (var track in tracksToFix)
            {
                if (string.IsNullOrWhiteSpace(track.Tag.Album))
                {
                    var directory = track.Name?
                        .Split("/")?[^2];

                    if (string.IsNullOrWhiteSpace(directory))
                    {
                        throw new MediaFixerException(MediaFixerException.Reason.MissingAlbumName);
                    }

                    track.Tag.Album = directory;
                }

                if (!albums.ContainsKey(track.Tag.Album))
                {
                    albums.Add(track.Tag.Album, new List<TagLib.File> { track });
                }
                else
                {
                    albums[track.Tag.Album].Add(track);
                }

                mediaFixerStatus.ItemProcessed();
            }

            return albums;
        }
    }
}
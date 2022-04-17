using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Data;

namespace MediaFixerLib.Fixer
{
    public static class TrackCountFixer
    {
        public static void FixTrackCounts(IEnumerable<ITrack> tracks)
        {
            if (tracks == null) throw new ArgumentNullException(nameof(tracks));

            var trackArray= tracks.ToArray();

            foreach (var track in trackArray)
            {
                track.TrackCount = trackArray.Length;
            }
        }
    }
}
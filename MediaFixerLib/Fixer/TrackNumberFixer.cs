using System;
using System.Collections.Generic;
using System.Linq;
using MediaFixerLib.Data;
using MediaFixerLib.Workflow;

namespace MediaFixerLib.Fixer
{
    public static class TrackNumberFixer
    {
        public static void FixTrackNumbers(IEnumerable<ITrack> tracks)
        {
            if (tracks == null) throw new ArgumentNullException(nameof(tracks));
        
            var trackList = tracks.ToList();
            var trackComparer = TrackComparerFactory.GetTrackComparer(trackList);
            trackList.Sort(trackComparer);

            for (var i = 0; i < trackList.Count; i++)
            {
                trackList[i].TrackNumber = i + 1;
            }
        }
    }
}
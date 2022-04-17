using System.Collections.Generic;
using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public static class TrackComparerFactory
    {
        public static IComparer<ITrack> GetTrackComparer(IEnumerable<ITrack> tracks)
        {
            var trackNumbers = new HashSet<string>();

            foreach (var track in tracks)
            {
                var key = track.GetKey();
                if (track.TrackNumber != 0 && !trackNumbers.Contains(key))
                {
                    trackNumbers.Add(key);
                }
                else
                {
                    return new TrackNameComparer();
                }
            }

            return new TrackDiscAndNumberComparer();
        }
    }
}
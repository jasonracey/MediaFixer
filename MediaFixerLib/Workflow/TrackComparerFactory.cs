using System.Collections.Generic;

namespace MediaFixerLib.Workflow
{
    public static class TrackComparerFactory
    {
        public static IComparer<TagLib.File> GetTrackComparer(IEnumerable<TagLib.File> tracks)
        {
            var trackNumbers = new HashSet<string>();

            foreach (var track in tracks)
            {
                var key = track.GetKey();
                if (track.Tag.Track != 0 && !trackNumbers.Contains(key))
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
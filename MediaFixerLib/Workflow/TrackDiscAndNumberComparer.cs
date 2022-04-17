using System.Collections.Generic;
using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public class TrackDiscAndNumberComparer : IComparer<ITrack>
    {
        public int Compare(ITrack? t1, ITrack? t2)
        {
            var discComparisonResult = CompareDiscNumbers(t1, t2);

            return discComparisonResult == 0 
                ? CompareTrackNumbers(t1, t2) 
                : discComparisonResult;
        }

        private static int CompareDiscNumbers(ITrack? t1, ITrack? t2)
        {
            return t1?.DiscNumber.CompareTo(t2?.DiscNumber) ?? 0;
        }

        private static int CompareTrackNumbers(ITrack? t1, ITrack? t2)
        {
            return t1?.TrackNumber.CompareTo(t2?.TrackNumber) ?? 0;
        }
    }
}
using System.Collections.Generic;
using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public class TrackNameComparer : IComparer<ITrack>
    {
        public int Compare(ITrack? t1, ITrack? t2)
        {
            return string.CompareOrdinal(t1?.TrackName, t2?.TrackName);
        }
    }
}
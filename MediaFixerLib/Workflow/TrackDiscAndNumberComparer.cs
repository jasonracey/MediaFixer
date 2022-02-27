namespace MediaFixerLib.Workflow;

public class TrackDiscAndNumberComparer : IComparer<TagLib.File>
{
    public int Compare(TagLib.File? t1, TagLib.File? t2)
    {
        var discComparisonResult = CompareDiscNumbers(t1, t2);

        return discComparisonResult == 0 
            ? CompareTrackNumbers(t1, t2) 
            : discComparisonResult;
    }

    private static int CompareDiscNumbers(TagLib.File? t1, TagLib.File? t2)
    {
        return t1?.Tag.Disc.CompareTo(t2?.Tag.Disc) ?? 0;
    }

    private static int CompareTrackNumbers(TagLib.File? t1, TagLib.File? t2)
    {
        return t1?.Tag.Track.CompareTo(t2?.Tag.Track) ?? 0;
    }
}
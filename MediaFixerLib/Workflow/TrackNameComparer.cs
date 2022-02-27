namespace MediaFixerLib.Workflow;

public class TrackNameComparer : IComparer<TagLib.File>
{
    public int Compare(TagLib.File? t1, TagLib.File? t2)
    {
        return string.CompareOrdinal(t1?.Name, t2?.Name);
    }
}
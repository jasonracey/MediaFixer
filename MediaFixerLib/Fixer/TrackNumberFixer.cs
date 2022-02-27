using MediaFixerLib.Workflow;

namespace MediaFixerLib.Fixer;

public static class TrackNumberFixer
{
    public static void FixTrackNumbers(IEnumerable<TagLib.File> tracks)
    {
        if (tracks == null) throw new ArgumentNullException(nameof(tracks));
        
        var trackList= tracks.ToList();
        var trackComparer = TrackComparerFactory.GetTrackComparer(trackList);
        trackList.Sort(trackComparer);

        for (var i = 0; i < trackList.Count; i++)
        {
            trackList[i].Tag.Track = (uint)i + 1;
        }
    }
}
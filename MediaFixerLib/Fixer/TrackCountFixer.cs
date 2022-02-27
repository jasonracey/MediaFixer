namespace MediaFixerLib.Fixer;

public static class TrackCountFixer
{
    public static void FixTrackCounts(IEnumerable<TagLib.File> tracks)
    {
        if (tracks == null) throw new ArgumentNullException(nameof(tracks));

        var trackArray= tracks.ToArray();
        var trackCount = (uint)trackArray.Length;

        foreach (var track in trackArray)
        {
            track.Tag.TrackCount = trackCount;
        }
    }
}
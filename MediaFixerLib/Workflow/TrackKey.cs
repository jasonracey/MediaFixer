namespace MediaFixerLib.Workflow;

public static class TrackKey
{
    public static string GetKey(this TagLib.File track)
    {
        return $"{track.Tag.Disc:D3}-{track.Tag.Disc:D3}";
    }
}
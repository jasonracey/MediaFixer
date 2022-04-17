using MediaFixerLib.Data;

namespace MediaFixerLib.Workflow
{
    public static class TrackKey
    {
        public static string GetKey(this ITrack track)
        {
            return $"{track.DiscNumber:D3}-{track.TrackNumber:D3}";
        }
    }
}
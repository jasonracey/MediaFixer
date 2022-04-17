namespace MediaFixerLib.Data
{
    public interface ITrack
    {
        void Save();
        string? AlbumName { get; set; }
        string? Comment { get; set; }
        int DiscNumber { get; set; }
        int DiscCount { get; set; }
        string FileName { get; }
        string? TrackName { get; set; }
        int TrackNumber { get; set; }
        int TrackCount { get; set; }
    }
}
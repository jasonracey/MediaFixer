using CommandLine;

namespace MediaFixerConsole
{
    public class Options
    {
        [Option('d', "directory", Required = true, HelpText = "The directory containing the media to fix.")]
        public string? Directory { get; set; }
    
        [Option('r', Required = false, HelpText = "Find and replace")]
        public (string OldValue, string NewValue)? FindAndReplace { get; set; }
    
        [Option('c', Required = false, HelpText = "Fix count of tracks on album")]
        public bool FixCountOfTracksOnAlbum { get; set; }
    
        [Option('g', Required = false, HelpText = "Fix Grateful Dead track names.")]
        public bool FixGratefulDeadTracks { get; set; }
    
        [Option('a', Required = false, HelpText = "Fix track name formatting.")]
        public bool FixTrackNames { get; set; }
    
        [Option('u', Required = false, HelpText = "Fix track numbers.")]
        public bool FixTrackNumbers { get; set; }
    
        [Option('i', Required = false, HelpText = "Import track names from the specified file.")]
        public string? ImportTrackNames { get; set; }
    
        [Option('m', Required = false, HelpText = "Merge the selected tracks into a single album.")]
        public bool MergeAlbums { get; set; }
    
        [Option('s', Required = false, HelpText = "Set the album name to the track name.")]
        public bool SetAlbumNames { get; set; }
    }
}
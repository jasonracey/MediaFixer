using System;

namespace MediaFixerLib.Data
{
    public class Track : ITrack
    {
        private readonly TagLib.File _file;

        public Track(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));
            
            _file = TagLib.File.Create(path);
        }

        public void Save()
        {
            _file.Save();
        }
        
        public string? AlbumName
        {
            get => _file.Tag.Album;
            set => _file.Tag.Album = value;
        }
        
        public string? Comment
        {
            get => _file.Tag.Comment;
            set => _file.Tag.Comment = value;
        }

        public int DiscNumber
        {
            get => (int)_file.Tag.Disc;
            set => _file.Tag.Disc = (uint)value;
        }
        
        public int DiscCount
        {
            get => (int)_file.Tag.DiscCount;
            set => _file.Tag.DiscCount = (uint)value;
        }
        
        public string FileName => _file.Name;
        
        public string? TrackName
        {
            get => _file.Tag.Title;
            set => _file.Tag.Title = value;
        }
        
        public int TrackNumber
        {
            get => (int)_file.Tag.Track;
            set => _file.Tag.Track = (uint)value;
        }
        
        public int TrackCount
        {
            get => (int)_file.Tag.TrackCount;
            set => _file.Tag.TrackCount = (uint)value;
        }
    }
}
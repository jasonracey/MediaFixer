using System;

namespace MediaFixerUI.Data
{
    public class Track
    {
        public int Number { get; }
        public int Count { get; }
        public string Title { get; }
        public string Album { get; }
        public int DiscNumber { get; }
        public int DiscCount { get; }

        public Track(TagLib.File file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            
            Number = (int)file.Tag.Track;
            Count = (int)file.Tag.TrackCount;
            Title = file.Tag.Title;
            Album = file.Tag.Album;
            DiscNumber = (int)file.Tag.Disc;
            DiscCount = (int)file.Tag.DiscCount;
        }
    }
}
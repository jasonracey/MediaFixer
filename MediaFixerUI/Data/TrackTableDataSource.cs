using System;
using System.Collections.Generic;
using AppKit;
using Foundation;
using MediaFixerLib.Data;

namespace MediaFixerUI.Data
{
    public class TrackTableDataSource : NSTableViewDataSource
    {
        public readonly List<ITrack> Tracks = new List<ITrack>();

        public override nint GetRowCount(NSTableView tableView)
        {
            return Tracks.Count;
        }

        public override void SortDescriptorsChanged (NSTableView tableView, NSSortDescriptor[] oldDescriptors)
        {
            if (oldDescriptors.Length > 0) 
            {
                Sort(oldDescriptors [0].Key, oldDescriptors [0].Ascending);
            } 
            else
            {
                var sortDescriptors = tableView.SortDescriptors; 
                Sort(sortDescriptors[0].Key, sortDescriptors[0].Ascending); 
            }
            tableView.ReloadData();
        }
        
        private void Sort(string key, bool asc) 
        {
            switch (key) 
            {
                case "Track #":
                    Tracks.Sort((t1, t2) => GetOrder(asc) * t1.TrackNumber.CompareTo(t2.TrackNumber));
                    break;
                case "Title":
                    Tracks.Sort((t1, t2) => GetOrder(asc) * string.Compare(t1.TrackName, t2.TrackName, StringComparison.Ordinal));
                    break;
                case "Album":
                    Tracks.Sort((t1, t2) => GetOrder(asc) * string.Compare(t1.AlbumName, t2.AlbumName, StringComparison.Ordinal));
                    break;
                case "Disc #":
                    Tracks.Sort((t1, t2) => GetOrder(asc) * t1.DiscNumber.CompareTo(t2.DiscNumber));
                    break;
            }
        }
        
        private static int GetOrder(bool asc)
        {
            return asc ? 1 : -1;
        }
    }
}
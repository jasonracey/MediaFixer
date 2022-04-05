using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace MediaFixerUI.Data
{
    public class TrackTableDataSource : NSTableViewDataSource
    {
        public readonly List<Track> Tracks = new List<Track>();

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
                    Tracks.Sort((x, y) => GetOrder(asc) * x.Number.CompareTo(y.Number));
                    break;
                case "Title":
                    Tracks.Sort((x, y) => GetOrder(asc) * string.Compare(x.Title, y.Title, StringComparison.Ordinal));
                    break;
                case "Album":
                    Tracks.Sort((x, y) => GetOrder(asc) * string.Compare(x.Album, y.Album, StringComparison.Ordinal));
                    break;
                case "Disc #":
                    Tracks.Sort((x, y) => GetOrder(asc) * x.DiscNumber.CompareTo(y.DiscNumber));
                    break;
            }
        }
        
        private static int GetOrder(bool asc)
        {
            return asc ? 1 : -1;
        }
    }
}
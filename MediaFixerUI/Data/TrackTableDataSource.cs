using System;
using System.Collections.Generic;
using AppKit;

namespace MediaFixerUI.Data
{
    public class TrackTableDataSource : NSTableViewDataSource
    {
        public readonly List<Track> Tracks = new List<Track>();

        public override nint GetRowCount(NSTableView tableView)
        {
            return Tracks.Count;
        }
    }
}
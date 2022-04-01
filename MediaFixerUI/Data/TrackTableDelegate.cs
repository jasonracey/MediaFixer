using System;
using AppKit;

namespace MediaFixerUI.Data
{
    public class TrackTableDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "TrackCell";

        private readonly TrackTableDataSource _dataSource;

        public TrackTableDelegate (TrackTableDataSource dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public override NSView GetViewForItem (
            NSTableView tableView, 
            NSTableColumn tableColumn, 
            nint row)
        {
            // This pattern allows you reuse existing views when they are no-longer in
            // use. If the returned view is null, you create a new view. If a non-null
            // view is returned, you modify it enough to reflect the new data.
            var view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (view == null)
            {
                view = new NSTextField ();
                view.Identifier = CellIdentifier;
                // ReSharper disable once HeuristicUnreachableCode
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            switch (tableColumn.Title) 
            {
                case "Track #":
                    var trackNumber = _dataSource.Tracks[(int)row].Number;
                    var trackCount = _dataSource.Tracks[(int)row].Count;
                    view.StringValue = $"{trackNumber} of {trackCount}";
                    view.Alignment = NSTextAlignment.Right;
                    break;
                case "Title":
                    view.StringValue = _dataSource.Tracks[(int)row].Title ?? "(null)";
                    break;
                case "Album":
                    view.StringValue = _dataSource.Tracks[(int)row].Album ?? "(null)";
                    break;
                case "Disc #":
                    var discNumber = _dataSource.Tracks[(int)row].DiscNumber;
                    var discCount = _dataSource.Tracks[(int)row].DiscCount;
                    view.StringValue = $"{discNumber} of {discCount}";
                    view.Alignment = NSTextAlignment.Right;
                    break;
            }

            return view;
        }
    }
}
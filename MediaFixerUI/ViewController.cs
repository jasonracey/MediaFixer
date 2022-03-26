using System;
using AppKit;
using Foundation;
using MediaFixerLib;
using MediaFixerLib.Workflow;

namespace MediaFixerUI
{
    public partial class ViewController : NSViewController
    {
        private static readonly MediaFixer MediaFixer = new MediaFixer(
            new MergeAlbumsWorkflowRunner(),
            new ImportTrackNamesWorkflowRunner(),
            new AlbumWorkflowRunner(),
            new TrackWorkflowRunner());
        
        private NSTimer _timer;
        
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Progress.Indeterminate = false;
            Status.StringValue = string.Empty;

            // select tracks box
            TextDirectory.Changed += DirectoryChanged;
            ButtonClearDirectory.Activated += ClearDirectory;
            ButtonSelectDirectory.Activated += SelectDirectory;
            
            // edit tracks tab
            RadioFixTracks.Activated += FixTracksToggled;
            RadioMergeAlbums.Activated += MergeAlbumsToggled;
            RadioSetAlbumNames.Activated += SetAlbumNamesToggled;
            ButtonStartEdit.Activated += StartEdit;
            
            // find and replace tab
            TextFind.Changed += FindChanged;
            ButtonClearFindReplace.Activated += ClearFindReplace;
            ButtonStartFindReplace.Activated += StartFindReplace;
            
            // import track names tab
            ButtonSelectFile.Activated += SelectFile;
            ButtonClearImportTrackNames.Activated += ClearImport;
            ButtonStartImportTrackNames.Activated += StartImport;
            
            SetIdleState();
        }
        
        #region Select Tracks Event Handlers
        private void DirectoryChanged(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
        
        private void ClearDirectory(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void SelectDirectory(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Edit Tracks Event Handlers
        private void FixTracksToggled(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void MergeAlbumsToggled(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void SetAlbumNamesToggled(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void StartEdit(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Find And Replace Event Handlers
        private void FindChanged(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
        
        private void ClearFindReplace(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void StartFindReplace(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        #region Import Track Names Event Handlers
        private void SelectFile(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void ClearImport(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void StartImport(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void SetBusyState()
        {
            // select tracks box
            TextDirectory.Editable = false;
            ButtonClearDirectory.Enabled = false;
            ButtonSelectDirectory.Enabled = false;
            
            // edit tracks tab
            RadioFixTracks.Enabled = false;
            CheckGratefulDead.Enabled = false;
            RadioMergeAlbums.Enabled = false;
            RadioSetAlbumNames.Enabled = false;
            ButtonStartEdit.Enabled = false;
            
            // find and replace tab
            TextFind.Editable = false;
            TextReplace.Editable = false;
            ButtonClearFindReplace.Enabled = false;
            ButtonStartFindReplace.Enabled = false;
            
            // import track names tab
            TextFile.Editable = false;
            ButtonClearImportTrackNames.Enabled = false;
            ButtonStartImportTrackNames.Enabled = false;
            ButtonSelectFile.Enabled = false;
            
            // feedback zone
            Progress.DoubleValue = 0.0D;
            Progress.Hidden = false;
            Status.StringValue = string.Empty;
            Status.TextColor = NSColor.White;
        }
        
        private void SetErrorState()
        {
            throw new NotImplementedException();
        }

        private void SetIdleState()
        {
            // select tracks box
            TextDirectory.Editable = false;
            TextDirectory.StringValue = string.Empty;
            ButtonClearDirectory.Enabled = false;
            ButtonSelectDirectory.Enabled = true;
            
            // edit tracks tab
            RadioFixTracks.Enabled = true;
            RadioFixTracks.State = NSCellStateValue.On;
            CheckGratefulDead.Enabled = true;
            CheckGratefulDead.State = NSCellStateValue.Off;
            RadioMergeAlbums.Enabled = true;
            RadioMergeAlbums.State = NSCellStateValue.Off;
            RadioSetAlbumNames.Enabled = true;
            RadioSetAlbumNames.State = NSCellStateValue.Off;
            ButtonStartEdit.Enabled = false;
            
            // find and replace tab
            TextFind.StringValue = string.Empty;
            TextReplace.StringValue = string.Empty;
            ButtonClearFindReplace.Enabled = false;
            ButtonStartFindReplace.Enabled = false;
            
            // import track names tab
            TextFile.Editable = false;
            TextFile.StringValue = string.Empty;
            ButtonClearImportTrackNames.Enabled = false;
            ButtonStartImportTrackNames.Enabled = false;
            ButtonSelectFile.Enabled = true;
            
            // feedback zone
            Progress.DoubleValue = 0.0D;
            Progress.Hidden = true;
            Status.StringValue = string.Empty;
            Status.TextColor = NSColor.White;
        }
    }
}
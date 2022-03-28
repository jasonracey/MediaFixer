using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppKit;
using Foundation;
using MediaFixerLib;
using MediaFixerLib.Workflow;

namespace MediaFixerUI
{
    public partial class ViewController : NSViewController
    {
        private const string DownloadsFolderName = "Downloads";
        private const string FileSearchPattern = "*.mp3";
        private const int OpenFileResult = 1;
        private const double TimerIntervalSeconds = 0.25D;

        private static readonly string DownloadsPath = Path.Combine(
            Environment.SpecialFolder.UserProfile.ToString(), 
            DownloadsFolderName);
        
        private static readonly  NSOpenPanel OpenPanel = new NSOpenPanel
        {
            AllowsMultipleSelection = false,
            CanChooseDirectories = true,
            CanChooseFiles = false,
            CanCreateDirectories = false,
            Directory = DownloadsPath,
            ReleasedWhenClosed = false,
            ShowsHiddenFiles = false,
            ShowsResizeIndicator = true
        };
        
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
            ButtonClearDirectory.Activated += ClearDirectory;
            ButtonSelectDirectory.Activated += SelectDirectory;
            
            // edit tracks tab
            RadioFixTracks.Activated += FixTracksClicked;
            RadioMergeAlbums.Activated += MergeAlbumsClicked;
            RadioSetAlbumNames.Activated += SetAlbumNamesClicked;
            
            // find and replace tab
            TextFind.Changed += FindChanged;
            ButtonClearFindReplace.Activated += ClearFindReplace;
            
            // import track names tab
            ButtonSelectFile.Activated += SelectFile;
            ButtonClearImportTrackNames.Activated += ClearImport;
            
            SetIdleState();
        }
        
        #region Select Tracks Event Handlers
        private void ClearDirectory(object sender, EventArgs e)
        {
            // select tracks box
            TextDirectory.StringValue = string.Empty;
            ButtonClearDirectory.Enabled = false;

            // find and replace tab
            ButtonStartFindReplace.Enabled = false;
            
            // import track names tab
            ButtonStartImportTrackNames.Enabled = false;
        }

        private void SelectDirectory(object sender, EventArgs e)
        {
            var result = OpenPanel.RunModal();
            if (result != OpenFileResult) return;
            
            // select tracks box
            TextDirectory.StringValue = OpenPanel.Url.Path;
            ButtonClearDirectory.Enabled = !string.IsNullOrWhiteSpace(TextDirectory.StringValue);
            
            // edit tab
            ButtonStartEdit.Enabled = RadioFixTracks.State == 
                NSCellStateValue.On || 
                RadioMergeAlbums.State == NSCellStateValue.On || 
                RadioSetAlbumNames.State == NSCellStateValue.On;

            // find and replace tab
            ButtonStartFindReplace.Enabled = !string.IsNullOrEmpty(TextFind.StringValue);
            
            // import track names tab
            ButtonStartImportTrackNames.Enabled = !string.IsNullOrWhiteSpace(TextFile.StringValue);
        }
        #endregion
        
        #region Edit Tracks Event Handlers
        private void FixTracksClicked(object sender, EventArgs e)
        {
            RadioFixTracks.State = NSCellStateValue.On;
            RadioMergeAlbums.State = NSCellStateValue.Off;
            RadioSetAlbumNames.State = NSCellStateValue.Off;
        }
        
        private void MergeAlbumsClicked(object sender, EventArgs e)
        {
            RadioFixTracks.State = NSCellStateValue.Off;
            RadioMergeAlbums.State = NSCellStateValue.On;
            RadioSetAlbumNames.State = NSCellStateValue.Off;
        }
        
        private void SetAlbumNamesClicked(object sender, EventArgs e)
        {
            RadioFixTracks.State = NSCellStateValue.Off;
            RadioMergeAlbums.State = NSCellStateValue.Off;
            RadioSetAlbumNames.State = NSCellStateValue.On;
        }

        async partial void StartEditClicked(NSButton sender)
        {
            var workflows = GetEditWorkflows(
                RadioFixTracks.State, 
                CheckGratefulDead.State,
                RadioMergeAlbums.State, 
                RadioSetAlbumNames.State);

            await RunWorkflows(workflows);
        }
        #endregion

        #region Find And Replace Event Handlers
        private void FindChanged(object sender, EventArgs args)
        {
            var haveText = !string.IsNullOrEmpty(TextFind.StringValue);
            ButtonClearFindReplace.Enabled = haveText;
            ButtonStartFindReplace.Enabled = HaveDirectory() && haveText;
        }
        
        private void ClearFindReplace(object sender, EventArgs e)
        {
            TextFind.StringValue = string.Empty;
            TextReplace.StringValue = string.Empty;
            ButtonClearFindReplace.Enabled = false;
            ButtonStartFindReplace.Enabled = false;
        }
        
        async partial void StartFindReplaceClicked(NSButton sender)
        {
            var workflows = new[]
            {
                Workflow.Create(
                    name: WorkflowName.FindAndReplace, 
                    oldValue: TextFind.StringValue, 
                    newValue: TextReplace.StringValue) 
            };
            
            await RunWorkflows(workflows);
        }
        #endregion
        
        #region Import Track Names Event Handlers
        private void ClearImport(object sender, EventArgs e)
        {
            TextFile.StringValue = string.Empty;
            ButtonClearImportTrackNames.Enabled = false;
            ButtonStartImportTrackNames.Enabled = false;
        }
        
        private void SelectFile(object sender, EventArgs e)
        {
            var result = OpenPanel.RunModal();
            if (result != OpenFileResult) return;
            
            TextFile.StringValue = OpenPanel.Url.Path;

            var haveFilePath = !string.IsNullOrWhiteSpace(TextFile.StringValue);
            ButtonClearImportTrackNames.Enabled = haveFilePath;
            ButtonStartImportTrackNames.Enabled = HaveDirectory() && haveFilePath;
        }

        async partial void StartImportClicked(NSButton sender)
        {
            var workflows = new[]
            {
                Workflow.Create(
                    WorkflowName.ImportTrackNames, 
                    fileName: TextFile.StringValue)
            };
            
            await RunWorkflows(workflows);
        }
        #endregion
        
        private static IEnumerable<Workflow> GetEditWorkflows(
            NSCellStateValue fixTracksState,
            NSCellStateValue fixGratefulDeadTracksState,
            NSCellStateValue mergeAlbumsState,
            NSCellStateValue setAlbumNamesState)
        {
            var workflows = new HashSet<Workflow>();

            if (fixTracksState == NSCellStateValue.On)
            {
                workflows.Add(Workflow.Create(name: WorkflowName.FixCountOfTracksOnAlbum));
                if (fixGratefulDeadTracksState == NSCellStateValue.On)
                {
                    workflows.Add(Workflow.Create(name: WorkflowName.FixGratefulDeadTracks));
                }
                workflows.Add(Workflow.Create(name: WorkflowName.FixTrackNames));
                workflows.Add(Workflow.Create(name: WorkflowName.FixTrackNumbers));
            }
            else if (mergeAlbumsState == NSCellStateValue.On)
            {
                workflows.Add(Workflow.Create(name: WorkflowName.MergeAlbums));
            }
            else if (setAlbumNamesState == NSCellStateValue.On)
            {
                workflows.Add(Workflow.Create(name: WorkflowName.SetAlbumNames));
            }
            else
            {
                throw new InvalidOperationException("One radio button must be in the on state");
            }

            return workflows;
        }
        
        private static IEnumerable<string> GetFilePaths(string directoryPath)
        {
            return Directory.GetFiles(
                directoryPath.Trim(), 
                FileSearchPattern, 
                SearchOption.AllDirectories);
        }
        
        private static double GetPercentCompleted(double completed, double total)
        {
            return total == 0.0D 
                ? 0.0D 
                : 100 * completed / total;
        }
        
        private bool HaveDirectory()
        {
            return !string.IsNullOrWhiteSpace(TextDirectory.StringValue);
        }
        
        private async Task RunWorkflows(IEnumerable<Workflow> workflows)
        {
            SetBusyState();
            StartTimer();

            try
            {
                var filePaths = GetFilePaths(TextDirectory.StringValue);

                await Task.Run(() => MediaFixer.FixMedia(
                    filePaths,
                    workflows));
                
                SetIdleState();
            }
            catch (Exception ex)
            {
                SetErrorState($"Error: {ex.Message}");
            }
            finally
            {
                StopTimer();
            }
        }

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
        
        private void SetErrorState(string message)
        {
            Status.StringValue = message;
            Status.TextColor = NSColor.Red;
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
            TextFind.Editable = true;
            TextFind.StringValue = string.Empty;
            TextReplace.Editable = true;
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
        
        private void StartTimer()
        {
            StopTimer();
            
            _timer = NSTimer.CreateRepeatingScheduledTimer(TimerIntervalSeconds, _ => {
                Progress.DoubleValue = GetPercentCompleted(MediaFixer.ItemsProcessed, MediaFixer.ItemsTotal);
                Status.StringValue = MediaFixer.Message;
            });
        }

        private void StopTimer()
        {
            if (_timer == null) return;
            _timer.Invalidate();
            _timer.Dispose();
            _timer = null;
        }
    }
}
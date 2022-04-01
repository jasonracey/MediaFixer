using AppKit;

namespace MediaFixerUI.Utilities
{
    public static class OpenPanelBuilder
    {
        public static NSOpenPanel Build(
            OpenPanelType openPanelType, 
            string directory)
        {
            return new NSOpenPanel
            {
                AllowsMultipleSelection = false,
                CanChooseDirectories = openPanelType == OpenPanelType.Directory,
                CanChooseFiles = openPanelType == OpenPanelType.File,
                CanCreateDirectories = false,
                Directory = directory,
                ReleasedWhenClosed = false,
                ShowsHiddenFiles = false,
                ShowsResizeIndicator = true
            };
        }
    }
}
// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MediaFixerUI
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton ButtonClearDirectory { get; set; }

		[Outlet]
		AppKit.NSButton ButtonClearFindReplace { get; set; }

		[Outlet]
		AppKit.NSButton ButtonClearImportTrackNames { get; set; }

		[Outlet]
		AppKit.NSButton ButtonSelectDirectory { get; set; }

		[Outlet]
		AppKit.NSButton ButtonSelectFile { get; set; }

		[Outlet]
		AppKit.NSButton ButtonStartEdit { get; set; }

		[Outlet]
		AppKit.NSButton ButtonStartFindReplace { get; set; }

		[Outlet]
		AppKit.NSButton ButtonStartImportTrackNames { get; set; }

		[Outlet]
		AppKit.NSButton CheckGratefulDead { get; set; }

		[Outlet]
		AppKit.NSButton RadioFixTracks { get; set; }

		[Outlet]
		AppKit.NSButton RadioMergeAlbums { get; set; }

		[Outlet]
		AppKit.NSButton RadioSetAlbumNames { get; set; }

		[Outlet]
		AppKit.NSTabViewItem TabEditTracks { get; set; }

		[Outlet]
		AppKit.NSTabViewItem TabFindAndReplace { get; set; }

		[Outlet]
		AppKit.NSTabViewItem TabImportTrackNames { get; set; }

		[Outlet]
		AppKit.NSTextField TextDirectory { get; set; }

		[Outlet]
		AppKit.NSTextField TextFile { get; set; }

		[Outlet]
		AppKit.NSTextField TextFind { get; set; }

		[Outlet]
		AppKit.NSTextField TextReplace { get; set; }

		[Action ("ButtonClearFindReplaceClicked:")]
		partial void ButtonClearFindReplaceClicked (Foundation.NSObject sender);

		[Action ("ButtonClearImportTrackNamesClicked:")]
		partial void ButtonClearImportTrackNamesClicked (Foundation.NSObject sender);

		[Action ("ButtonSelectFileClicked:")]
		partial void ButtonSelectFileClicked (Foundation.NSObject sender);

		[Action ("ButtonStartEditClicked:")]
		partial void ButtonStartEditClicked (Foundation.NSObject sender);

		[Action ("ButtonStartFindReplaceClicked:")]
		partial void ButtonStartFindReplaceClicked (Foundation.NSObject sender);

		[Action ("ButtonStartImportTrackNamesClicked:")]
		partial void ButtonStartImportTrackNamesClicked (Foundation.NSObject sender);

		[Action ("FindTextChanged:")]
		partial void FindTextChanged (Foundation.NSObject sender);

		[Action ("FixTracksClicked:")]
		partial void FixTracksClicked (Foundation.NSObject sender);

		[Action ("FixTracksSelected:")]
		partial void FixTracksSelected (Foundation.NSObject sender);

		[Action ("FixTracksToggled:")]
		partial void FixTracksToggled (Foundation.NSObject sender);

		[Action ("MergeAlbumsToggled:")]
		partial void MergeAlbumsToggled (Foundation.NSObject sender);

		[Action ("ResetClicked:")]
		partial void ResetClicked (Foundation.NSObject sender);

		[Action ("SelectDirectoryClicked:")]
		partial void SelectDirectoryClicked (Foundation.NSObject sender);

		[Action ("SetAlbumNamesSelected:")]
		partial void SetAlbumNamesSelected (Foundation.NSObject sender);

		[Action ("SetAlbumNamesToggled:")]
		partial void SetAlbumNamesToggled (Foundation.NSObject sender);

		[Action ("TextDirectoryChanged:")]
		partial void TextDirectoryChanged (Foundation.NSObject sender);

		[Action ("TextFindChanged:")]
		partial void TextFindChanged (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (TextDirectory != null) {
				TextDirectory.Dispose ();
				TextDirectory = null;
			}

			if (ButtonClearDirectory != null) {
				ButtonClearDirectory.Dispose ();
				ButtonClearDirectory = null;
			}

			if (ButtonSelectDirectory != null) {
				ButtonSelectDirectory.Dispose ();
				ButtonSelectDirectory = null;
			}

			if (TabEditTracks != null) {
				TabEditTracks.Dispose ();
				TabEditTracks = null;
			}

			if (RadioFixTracks != null) {
				RadioFixTracks.Dispose ();
				RadioFixTracks = null;
			}

			if (CheckGratefulDead != null) {
				CheckGratefulDead.Dispose ();
				CheckGratefulDead = null;
			}

			if (RadioMergeAlbums != null) {
				RadioMergeAlbums.Dispose ();
				RadioMergeAlbums = null;
			}

			if (RadioSetAlbumNames != null) {
				RadioSetAlbumNames.Dispose ();
				RadioSetAlbumNames = null;
			}

			if (ButtonStartEdit != null) {
				ButtonStartEdit.Dispose ();
				ButtonStartEdit = null;
			}

			if (TabFindAndReplace != null) {
				TabFindAndReplace.Dispose ();
				TabFindAndReplace = null;
			}

			if (TextFind != null) {
				TextFind.Dispose ();
				TextFind = null;
			}

			if (TextReplace != null) {
				TextReplace.Dispose ();
				TextReplace = null;
			}

			if (ButtonClearFindReplace != null) {
				ButtonClearFindReplace.Dispose ();
				ButtonClearFindReplace = null;
			}

			if (ButtonStartFindReplace != null) {
				ButtonStartFindReplace.Dispose ();
				ButtonStartFindReplace = null;
			}

			if (TabImportTrackNames != null) {
				TabImportTrackNames.Dispose ();
				TabImportTrackNames = null;
			}

			if (TextFile != null) {
				TextFile.Dispose ();
				TextFile = null;
			}

			if (ButtonSelectFile != null) {
				ButtonSelectFile.Dispose ();
				ButtonSelectFile = null;
			}

			if (ButtonClearImportTrackNames != null) {
				ButtonClearImportTrackNames.Dispose ();
				ButtonClearImportTrackNames = null;
			}

			if (ButtonStartImportTrackNames != null) {
				ButtonStartImportTrackNames.Dispose ();
				ButtonStartImportTrackNames = null;
			}

		}
	}
}

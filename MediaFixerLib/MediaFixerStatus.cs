namespace MediaFixerLib;

public class MediaFixerStatus
{
    public const string AssigningTrackNames = "Assigning new track names...";
    public const string GeneratingAlbumList = "Generating album list...";
    public const string GeneratingAlbumGroups = "Generating album groups...";
    public const string LoadingSelectedFiles = "Loading selected files...";
    public const string ReadingTrackNames = "Reading track names to import...";
    public const string RunningAlbumWorkflows = "Running album workflows...";
    public const string RunningMergeAlbums = "Running merge albums workflow...";
    public const string RunningTrackWorkflows = "Running track workflows...";
    
    public int ItemsProcessed { get; private set; }
    public int ItemsTotal { get; }
    public string Message { get; }
    private MediaFixerStatus(int itemsTotal, string? message = null)
    {
        ItemsTotal = itemsTotal;
        Message = message ?? string.Empty;
    }

    public static MediaFixerStatus Create(int itemsTotal, string? message = null)
    {
        if (itemsTotal < 0) throw new ArgumentOutOfRangeException(nameof(itemsTotal), "Value must be >= 0");
        return new MediaFixerStatus(itemsTotal, message);
    }

    public void ItemProcessed()
    {
        ItemsProcessed++;
    }
}
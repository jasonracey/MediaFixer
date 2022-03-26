namespace MediaFixerLib.Fixer
{
    public static class AlbumNameFixer
    {
        public static string FixAlbumName(this string albumName)
        {
            return albumName
                .RemoveDiscNumber()
                .Trim()
                .RemoveDoubleSpaces()
                .ToTitleCase()
                .FixRomanNumerals()
                .FixRegionAbbreviation();
        }
    }
}
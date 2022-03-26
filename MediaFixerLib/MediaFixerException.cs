using System;

namespace MediaFixerLib
{
    [Serializable]
    public class MediaFixerException : Exception
    {
        public static class Reason
        {
            public const string ImportCountMismatch = "The number of names to import must match the number of tracks selected";
            public const string MissingAlbumName = "One or more tracks is missing an album name";
            public const string MissingTrackComment = "One or more Grateful Dead tracks is missing a comment";
            public const string MissingTrackNumber = "One or more tracks does not have a track number";
        }

        public MediaFixerException() { }
        public MediaFixerException(string message) : base(message) { }
        public MediaFixerException(string message, Exception inner) : base(message, inner) { }
        protected MediaFixerException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
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
        }

        public MediaFixerException() { }
        public MediaFixerException(string message) : base(message) { }
        public MediaFixerException(string message, Exception inner) : base(message, inner) { }
        protected MediaFixerException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
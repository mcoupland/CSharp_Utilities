using System;

namespace Utilities
{
    public class Exceptions
    {
        public class InvalidArgumentException : Exception
        {
            public InvalidArgumentException(string message) : base(message) { }
        }
        public class PictureManipulationException : Exception
        {
            public PictureManipulationException(string message) : base(message) { }
        }
        public class FileExistsException : Exception
        {
            public FileExistsException(string message) : base(message) { }
        }
        public class SerializationException : Exception
        {
            public SerializationException(string message) : base(message) { }
        }
    }
}

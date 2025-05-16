using System;

namespace Common.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException() : base("Invalid paramter(s)")
        {
        }

        public InvalidArgumentException(string errorMessage) : base(errorMessage)
        {
        }
    }
}

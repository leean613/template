using System;

namespace Common.Exceptions
{
    public class DuplicateUserNameException : Exception
    {
        public DuplicateUserNameException() : base("Duplicate user name.")
        {
        }

        public DuplicateUserNameException(string errorMessage) : base(errorMessage)
        {
        }
    }
}

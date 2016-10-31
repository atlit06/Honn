using System;

namespace Assignment3.Services.Exceptions
{
    public class AppValidationException : Exception
    {
        public AppValidationException()
        {

        }
        public AppValidationException(string message) : base(message)
        {
        }
        public AppValidationException(string message, Exception inner) : base(message, inner)
        {
        }
    }    
}

